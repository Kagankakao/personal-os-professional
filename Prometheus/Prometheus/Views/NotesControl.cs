using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;

namespace KeganOS.Views
{
    public partial class NotesControl : XtraUserControl
    {
        private readonly INoteService _noteService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<NotesControl>();
        private NoteItem? _currentNote;
        private List<NoteItem> _notes = new();
        private List<NoteItem> _filteredNotes = new();
        private readonly string _imagesFolder;
        
        // Animation & UI state
        private int _hoverIndex = -1;
        private System.Windows.Forms.Timer _tmrAutoSave;

        public NotesControl(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
            
            // Setup images folder
            _imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "KeganOS", "NoteImages");
            // Directory creation moved to OnLoad to prevent constructor crashes
            
            InitializeComponent(); // Layout updated
            
            // Correction for "White Screen" issue: Force Dark Background
            ApplyTheme();
            
            // Ensure child controls don't override with white skin
            pnlHeader.Appearance.BackColor = Color.Transparent; 
            pnlHeader.Appearance.Options.UseBackColor = true;
            pnlHeader.BorderStyle = BorderStyles.NoBorder;

            // Wire up events
            this.listNotes.SelectedIndexChanged += ListNotes_SelectedIndexChanged;
            this.btnNewNote.Click += BtnNewNote_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.txtContent.EditValueChanged += (s, e) => { UpdateWordCount(); StartAutoSaveTimer(); };
            this.txtTitle.EditValueChanged += (s, e) => { StartAutoSaveTimer(); };
            this.txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
            
            // Search Debounce Timer
            this.tmrSearchDebounce.Interval = 350;
            this.tmrSearchDebounce.Tick += (s, e) => {
                tmrSearchDebounce.Stop();
                ApplyFilter();
            };
            
            // Drag-drop events
            this.pnlImages.DragEnter += OnDragEnter;
            this.pnlImages.DragDrop += OnDragDrop;
            
            this.txtContent.AllowDrop = true;
            this.txtContent.DragEnter += OnDragEnter;
            this.txtContent.DragDrop += OnDragDrop;
            
            this.txtContent.AllowDrop = true;
            this.txtContent.DragEnter += OnDragEnter;
            this.txtContent.DragDrop += OnDragDrop;

            // Glassmorphism Paint
            this.pnlHeader.Paint += PnlHeader_Paint;
            
            // Custom Card Draw
            this.listNotes.DrawItem += ListNotes_DrawItem;
            this.listNotes.MouseMove += (s, e) => {
                var index = listNotes.IndexFromPoint(e.Location);
                if (index != _hoverIndex) {
                    _hoverIndex = index;
                    listNotes.Invalidate();
                }
            };
            this.listNotes.MouseLeave += (s, e) => {
                _hoverIndex = -1;
                listNotes.Invalidate();
            };


            // Auto-Save Timer
            _tmrAutoSave = new System.Windows.Forms.Timer { Interval = 300 }; // 0.3 seconds (Very Frequent)
            _tmrAutoSave.Tick += TmrAutoSave_Tick;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            try 
            {
                // Ensure directory exists
                if (!Directory.Exists(_imagesFolder))
                {
                    Directory.CreateDirectory(_imagesFolder);
                }

                await RefreshNotesListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error loading notes");
                XtraMessageBox.Show($"Could not load notes: {ex.Message}", "Error");
            }
        }

        private async System.Threading.Tasks.Task RefreshNotesListAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                _notes = await _noteService.GetNotesAsync(user.Id);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh notes list");
            }
        }

        private void ApplyFilter()
        {
            string filter = txtSearch.Text?.Trim().ToLower() ?? "";
            
            if (string.IsNullOrEmpty(filter))
            {
                _filteredNotes = _notes.OrderByDescending(n => n.LastModified).ToList();
            }
            else
            {
                // Token-based search (AND logic)
                var terms = filter.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                _filteredNotes = _notes
                    .Where(n => terms.All(term => 
                        n.Title.ToLower().Contains(term) || 
                        (n.Content?.ToLower().Contains(term) ?? false)))
                    .OrderByDescending(n => n.LastModified)
                    .ToList();
            }
            
            listNotes.Items.Clear();
            foreach (var note in _filteredNotes)
            {
                // We add the object itself, or a dummy string since we use CustomDrawItem
                listNotes.Items.Add(note);
            }
            
            pnlNotesList.Text = $"Neural Echoes ({_filteredNotes.Count})";
        }

        private void TxtSearch_EditValueChanged(object? sender, EventArgs e)
        {
            tmrSearchDebounce.Stop();
            tmrSearchDebounce.Start();
        }

        private void ListNotes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listNotes.SelectedItem is NoteItem selectedNote)
            {
                _currentNote = selectedNote;
                txtTitle.Text = _currentNote.Title;
                txtContent.Text = _currentNote.Content;
                UpdateWordCount();
                UpdateLastSaved();
                RefreshImages();
                
                // Focus logic - Only focus content if we are NOT searching
                if (!txtSearch.ContainsFocus)
                {
                    txtContent.Focus();
                    if (txtContent.Text.Length > 0)
                        txtContent.SelectionStart = txtContent.Text.Length;
                }
                
                txtTitle.Properties.Appearance.Font = new Font("Segoe UI Light", 24F);
            }
        }

        private void BtnNewNote_Click(object? sender, EventArgs e)
        {
            _currentNote = new NoteItem 
            { 
                Id = Guid.NewGuid().ToString(),
                Title = "", 
                Content = "",
                CreatedAt = DateTime.Now,
                LastModified = DateTime.Now
            };
            txtTitle.Text = "";
            txtContent.Text = "";
            txtTitle.Focus();
            UpdateWordCount();
            lblLastSaved.Text = "Unsaved";
            RefreshImages();
        }

        private async void BtnSave_Click(object? sender, EventArgs e)
        {
             await SaveCurrentNoteAsync(showFeedback: true);
        }

        private void StartAutoSaveTimer()
        {
            _tmrAutoSave.Stop();
            _tmrAutoSave.Start();
        }

        private async void TmrAutoSave_Tick(object? sender, EventArgs e)
        {
            _tmrAutoSave.Stop();
            if (_currentNote != null)
            {
               lblLastSaved.Text = "Saving...";
               await SaveCurrentNoteAsync(showFeedback: false);
            }
        }

        private async System.Threading.Tasks.Task SaveCurrentNoteAsync(bool showFeedback)
        {
            if (_currentNote == null) 
            {
                _currentNote = new NoteItem { 
                    Id = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.Now,
                    LastModified = DateTime.Now
                };
            }
            if (string.IsNullOrEmpty(_currentNote.Id))
            {
                _currentNote.Id = Guid.NewGuid().ToString();
            }

            // Get title from input or first line
            string title = txtTitle.Text.Trim();
            if (string.IsNullOrEmpty(title))
            {
                string firstLine = txtContent.Text.Split('\n').FirstOrDefault(l => !string.IsNullOrWhiteSpace(l)) ?? "Untitled Note";
                title = firstLine.Trim();
                if (title.Length > 30) title = title.Substring(0, 27) + "...";
            }
            
            _currentNote.Title = title;
            _currentNote.Content = txtContent.Text;
            _currentNote.LastModified = DateTime.Now;

            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                await _noteService.SaveNoteAsync(user.Id, _currentNote);
                await RefreshNotesListAsync();
                UpdateLastSaved();
                
                if (showFeedback)
                {
                    lblLastSaved.Text = "✓ Saved manually";
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save note");
                if (showFeedback)
                {
                    XtraMessageBox.Show("Failed to save note: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    lblLastSaved.Text = "Save failed";
                }
            }
        }

        private async void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_currentNote == null || string.IsNullOrEmpty(_currentNote.Id)) return;
            
            var result = XtraMessageBox.Show(
                $"Are you sure you want to delete \"{_currentNote.Title}\"?", 
                "Confirm Delete",
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);
            
            if (result != DialogResult.Yes) return;

            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                await _noteService.DeleteNoteAsync(_currentNote.Id);
                _currentNote = null;
                txtTitle.Text = "";
                txtContent.Text = "";
                await RefreshNotesListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to delete note");
                XtraMessageBox.Show("Failed to delete note: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateWordCount()
        {
            string text = txtContent.Text ?? "";
            int words = string.IsNullOrWhiteSpace(text) ? 0 : 
                text.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            int chars = text.Length;
            lblWordCount.Text = $"{words} words • {chars} chars";
        }

        private void UpdateLastSaved()
        {
            if (_currentNote != null)
            {
                var diff = DateTime.Now - _currentNote.LastModified;
                if (diff.TotalMinutes < 1)
                    lblLastSaved.Text = "Saved just now";
                else if (diff.TotalHours < 1)
                    lblLastSaved.Text = $"Saved {(int)diff.TotalMinutes}m ago";
                else if (diff.TotalDays < 1)
                    lblLastSaved.Text = $"Saved {(int)diff.TotalHours}h ago";
                else
                    lblLastSaved.Text = $"Saved {_currentNote.LastModified:MMM d}";
            }
            else
            {
                lblLastSaved.Text = "";
            }
        }

        private void PnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = pnlHeader.ClientRectangle;

            // Soft White/Blue Gradient
            using (var brush = new LinearGradientBrush(rect, 
                Color.FromArgb(245, 247, 250), 
                Color.FromArgb(230, 235, 245), 45f))
            {
                g.FillRectangle(brush, rect);
            }

            // Subtle Accent Line (Bottom)
            using (var pen = new Pen(Color.FromArgb(50, 0, 120, 215), 1)) 
            {
                g.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
            }
        }

        private void ListNotes_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _filteredNotes.Count) return;
            
            var note = _filteredNotes[e.Index];
            var isSelected = (e.State & DrawItemState.Selected) != 0;
            var isHovered = e.Index == _hoverIndex;
            
            // 1. Draw Background Card
            var rect = e.Bounds;
            rect.Inflate(-5, -5);

            Color bgColor = Color.FromArgb(255, 255, 255);
            if (isSelected) bgColor = Color.FromArgb(230, 240, 255); // Light Blue Selection
            else if (isHovered) bgColor = Color.FromArgb(245, 245, 250);

            e.Cache.FillRectangle(bgColor, rect);

            // Selection Edge
            if (isSelected)
            {
                e.Cache.DrawLine(new Pen(Color.FromArgb(0, 120, 215), 3), new Point(rect.X, rect.Y), new Point(rect.X, rect.Bottom));
            }
            else
            {
                // Subtle border for cards in light mode
                e.Cache.DrawRectangle(new Pen(Color.FromArgb(220, 220, 225), 1), rect);
            }

            // 2. Draw Text (Title)
            using (var fontTitle = new Font("Segoe UI Semibold", 11))
            {
                e.Cache.DrawString(note.Title ?? "Untitled", fontTitle, new SolidBrush(Color.FromArgb(40, 40, 40)), rect.X + 15, rect.Y + 12);
            }

            // 3. Draw Subtext (Date & Preview)
            string dateStr = note.LastModified.ToString("MMM d, HH:mm");
            string previewText = note.Content?.Replace("\r", "").Replace("\n", " ") ?? "";
            if (previewText.Length > 35) previewText = previewText.Substring(0, 32) + "...";

            using (var fontSmall = new Font("Segoe UI", 8.5f))
            {
                e.Cache.DrawString(dateStr, fontSmall, Brushes.Gray, rect.X + 15, rect.Y + 35);
                e.Cache.DrawString(previewText, fontSmall, Brushes.DimGray, rect.X + 15, rect.Y + 55);

                // Pinned indicator
                if (note.IsPinned)
                {
                    e.Cache.DrawString("★", fontSmall, Brushes.Orange, rect.Right - 25, rect.Y + 12);
                }
            }

            e.Handled = true;
        }

        // Manual Footer Painting removed in favor of controls

        // ═══════════════════════════════════════════════════════════════════
        // IMAGE DRAG & DROP
        // ═══════════════════════════════════════════════════════════════════
        
        private void OnDragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null && files.Any(IsImageFile))
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void OnDragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) != true) return;
            
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files == null) return;
            
            AddImages(files);
        }

        private bool IsImageFile(string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ext is ".png" or ".jpg" or ".jpeg" or ".gif" or ".bmp" or ".webp";
        }


        private void AddImages(IEnumerable<string> files)
        {
            // Ensure we have a note
            if (_currentNote == null)
            {
                _currentNote = new NoteItem { CreatedAt = DateTime.Now, Id = Guid.NewGuid().ToString() };
            }
            
            foreach (var file in files.Where(IsImageFile))
            {
                try
                {
                    var ext = Path.GetExtension(file);
                    var newName = $"{Guid.NewGuid()}{ext}";
                    var destPath = Path.Combine(_imagesFolder, newName);
                    File.Copy(file, destPath);
                    _currentNote.ImagePaths.Add(destPath);
                    _logger.Information("Image added: {Path}", destPath);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to copy image: {Path}", file);
                }
            }
            
            RefreshImages();
            StartAutoSaveTimer();
        }

        private void RefreshImages()
        {
            if (tableEditor == null) return;
            
            bool hasImages = _currentNote?.ImagePaths != null && _currentNote.ImagePaths.Any();
            
            tableEditor.SuspendLayout();
            try
            {
                // Only show the image panel if there are images
                pnlImages.Visible = hasImages;
                
                // Update TableLayoutPanel Row Height for Images (Now Row 1)
                tableEditor.RowStyles[1].Height = hasImages ? 135F : 0F;
                
                // Clear existing controls
                pnlImages.Controls.Clear();
                
                if (hasImages)
                {
                    // Header for the gallery
                    var lblHeader = new LabelControl
                    {
                        Text = "ATTACHED MEDIA",
                        Font = new Font("Segoe UI Semibold", 7f),
                        ForeColor = Color.DarkGray,
                        Margin = new Padding(0, 0, 0, 5),
                        AutoSizeMode = LabelAutoSizeMode.Default
                    };
                    pnlImages.SetFlowBreak(lblHeader, true);
                    pnlImages.Controls.Add(lblHeader);
                    
                    foreach (var imagePath in _currentNote.ImagePaths)
                    {
                        if (!File.Exists(imagePath)) continue;
                        
                        try
                        {
                            var pic = new PictureBox
                            {
                                Size = new Size(140, 105), // Consistent 4:3 aspect ratio
                                SizeMode = PictureBoxSizeMode.Zoom,
                                Image = Image.FromFile(imagePath),
                                Margin = new Padding(0, 0, 15, 0),
                                Cursor = Cursors.Hand,
                                Tag = imagePath,
                                BorderStyle = BorderStyle.FixedSingle,
                                BackColor = Color.FromArgb(245, 245, 250)
                            };
                            
                            // Tooltip for instructions
                            var toolTip = new ToolTip();
                            toolTip.SetToolTip(pic, "Click: View | Right-Click: Remove");
                            
                            pic.MouseClick += (s, e) =>
                            {
                                if (e.Button == MouseButtons.Right && _currentNote != null)
                                {
                                    if (XtraMessageBox.Show("Remove this image reference?", "Confirm", 
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        _currentNote.ImagePaths.Remove(imagePath);
                                        RefreshImages();
                                    }
                                }
                                else if (e.Button == MouseButtons.Left)
                                {
                                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                                    {
                                        FileName = imagePath,
                                        UseShellExecute = true
                                    });
                                }
                            };
                            
                            pnlImages.Controls.Add(pic);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Failed to load image: {Path}", imagePath);
                        }
                    }
                }
            }
            finally
            {
                tableEditor.ResumeLayout(true);
                tableEditor.PerformLayout();
            }
        }
        private void ApplyTheme()
        {
            Color lightBg = Color.FromArgb(250, 250, 255);
            Color panelBg = Color.FromArgb(240, 242, 245);
            Color textDark = Color.FromArgb(30, 30, 30);

            // 1. Main Background
            this.Appearance.BackColor = lightBg;
            this.Appearance.Options.UseBackColor = true;

            // 2. Split Container
            if (splitMain != null)
            {
                splitMain.Appearance.BackColor = Color.FromArgb(220, 220, 225);
                splitMain.Appearance.Options.UseBackColor = true;
            }

            // 3. Notes List Panel
            if (pnlNotesList != null)
            {
                pnlNotesList.LookAndFeel.UseDefaultLookAndFeel = false;
                pnlNotesList.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                pnlNotesList.Appearance.BackColor = panelBg;
                pnlNotesList.Appearance.Options.UseBackColor = true;
                pnlNotesList.AppearanceCaption.ForeColor = textDark;
                pnlNotesList.BorderStyle = BorderStyles.NoBorder;
            }

            // 4. Note Editor Panel
            if (pnlEditor != null)
            {
                pnlEditor.LookAndFeel.UseDefaultLookAndFeel = false;
                pnlEditor.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                pnlEditor.Appearance.BackColor = lightBg;
                pnlEditor.Appearance.Options.UseBackColor = true;
                pnlEditor.AppearanceCaption.ForeColor = textDark;
                pnlEditor.BorderStyle = BorderStyles.NoBorder;
                pnlEditor.ShowCaption = false; 
            }

            // 5. Editors
            if (txtTitle != null)
            {
                txtTitle.Properties.Appearance.BackColor = lightBg;
                txtTitle.Properties.Appearance.ForeColor = textDark;
                txtTitle.Properties.BorderStyle = BorderStyles.NoBorder;
            }

            if (txtContent != null)
            {
                txtContent.Properties.Appearance.BackColor = lightBg;
                txtContent.Properties.Appearance.ForeColor = textDark;
                txtContent.Properties.BorderStyle = BorderStyles.NoBorder;
            }

            // 6. List Box
            if (listNotes != null)
            {
                listNotes.Appearance.BackColor = panelBg;
                listNotes.Appearance.Options.UseBackColor = true;
                listNotes.BorderStyle = BorderStyles.NoBorder;
            }
            
            // 7. Search Box
            if (txtSearch != null)
            {
                txtSearch.Properties.Appearance.BackColor = Color.White;
                txtSearch.Properties.Appearance.ForeColor = textDark;
                txtSearch.Properties.BorderStyle = BorderStyles.Simple;
            }

            // 9. Buttons
            if (btnSave != null)
            {
                btnSave.Appearance.ForeColor = Color.FromArgb(0, 120, 215);
            }


            // 11. Header Label
            if (lblTitle != null)
            {
                lblTitle.ForeColor = textDark;
            }

            // 12. Layout Table
            if (tableEditor != null)
            {
                tableEditor.BackColor = lightBg;
            }

            // 13. Images Gallery
            if (pnlImages != null)
            {
                pnlImages.BackColor = lightBg;
            }
        }
    }
}
