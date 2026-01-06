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
        private System.Windows.Forms.Timer _tmrPulse;
        private float _pulseOpacity = 0.5f;
        private bool _pulseIncreasing = true;
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
            
            InitializeComponent();
            
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
            
            this.lblDropHint.AllowDrop = true;
            this.lblDropHint.DragEnter += OnDragEnter;
            this.lblDropHint.DragDrop += OnDragDrop;
            
            // Add Image button click
            this.lblDropHint.Click += LblDropHint_Click;
            this.lblDropHint.Cursor = Cursors.Hand;

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

            // Neural Sync Pulse
            _tmrPulse = new System.Windows.Forms.Timer { Interval = 50 };
            _tmrPulse.Tick += (s, e) => {
                if (_pulseIncreasing) _pulseOpacity += 0.05f;
                else _pulseOpacity -= 0.05f;

                if (_pulseOpacity >= 1.0f) _pulseIncreasing = false;
                if (_pulseOpacity <= 0.3f) _pulseIncreasing = true;
                
                pnlEditorFooter.Invalidate();
            };
            _tmrPulse.Start();

            this.pnlEditorFooter.Paint += PnlEditorFooter_Paint;

            // Auto-Save Timer
            _tmrAutoSave = new System.Windows.Forms.Timer { Interval = 2000 }; // 2 seconds
            _tmrAutoSave.Tick += TmrAutoSave_Tick;

            // UI Fix: Spacer to prevent overlap
            var pnlSpacer = new PanelControl();
            pnlSpacer.Height = 15;
            pnlSpacer.Dock = DockStyle.Top;
            pnlSpacer.BorderStyle = BorderStyles.NoBorder;
            pnlSpacer.Appearance.BackColor = Color.Transparent;
            pnlSpacer.Appearance.Options.UseBackColor = true;
            
            // Adjust Dock Order
            this.pnlEditor.Controls.Add(pnlSpacer);
            pnlSpacer.BringToFront(); // Ensure it acts relative to others
            this.pnlImages.SendToBack();
            this.txtTitle.SendToBack(); // Top-most doc
            this.pnlEditorFooter.SendToBack(); // Bottom
            
            // Re-ordering logic to be precise:
            // Controls.Add adds to the beginning of the list (z-order top).
            // For Dock=Top, the last added control is at the Top? No, Reverse.
            // Let's just create the spacer and ensure it sits between Title and Content.
            // Content is Dock.Fill. Title is Dock.Top. Images is Dock.Top.
            // If we add pnlSpacer (Dock.Top), it needs to be added *after* Title (so it's below it) or *before* Content.
            
            // Correct approach: Set the padding on the Content text box container? 
            // Better: Add padding to the Dock=Top elements.
            txtTitle.Padding = new Padding(0, 0, 0, 10);
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
            if (listNotes.SelectedIndex >= 0 && listNotes.SelectedIndex < _filteredNotes.Count)
            {
                _currentNote = _filteredNotes[listNotes.SelectedIndex];
                txtTitle.Text = _currentNote.Title;
                txtContent.Text = _currentNote.Content;
                UpdateWordCount();
                UpdateLastSaved();
                RefreshImages();
                
                // Focus logic
                txtContent.Focus();
                if (txtContent.Text.Length > 0)
                    txtContent.SelectionStart = txtContent.Text.Length;
                
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
                CreatedAt = DateTime.Now 
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
                    CreatedAt = DateTime.Now 
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

            // Glass Background (Simulated)
            using (var brush = new LinearGradientBrush(rect, 
                Color.FromArgb(60, 255, 255, 255), 
                Color.FromArgb(10, 255, 255, 255), 45f))
            {
                g.FillRectangle(brush, rect);
            }

            // Accent Line (Bottom)
            using (var pen = new Pen(Color.FromArgb(100, 0, 255, 255), 2)) // Cyan glow
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

            Color bgColor = Color.FromArgb(30, 30, 35);
            if (isSelected) bgColor = Color.FromArgb(45, 0, 150, 150); // Muted Cyan
            else if (isHovered) bgColor = Color.FromArgb(40, 40, 45);

            e.Cache.FillRectangle(bgColor, rect);

            // Selection Edge
            if (isSelected)
            {
                e.Cache.DrawLine(new Pen(Color.Cyan, 2), new Point(rect.X, rect.Y), new Point(rect.X, rect.Bottom));
            }

            // 2. Draw Text (Title)
            using (var fontTitle = new Font("Segoe UI Semibold", 11))
            {
                e.Cache.DrawString(note.Title ?? "Untitled", fontTitle, Brushes.White, rect.X + 15, rect.Y + 12);
            }

            // 3. Draw Subtext (Date & Preview)
            string dateStr = note.LastModified.ToString("MMM d, HH:mm");
            string previewText = note.Content?.Replace("\r", "").Replace("\n", " ") ?? "";
            if (previewText.Length > 35) previewText = previewText.Substring(0, 32) + "...";

            using (var fontSmall = new Font("Segoe UI", 8.5f))
            {
                e.Cache.DrawString(dateStr, fontSmall, Brushes.Gray, rect.X + 15, rect.Y + 35);
                e.Cache.DrawString(previewText, fontSmall, Brushes.Silver, rect.X + 15, rect.Y + 55);

                // Pinned indicator
                if (note.IsPinned)
                {
                    e.Cache.DrawString("★", fontSmall, Brushes.Gold, rect.Right - 25, rect.Y + 12);
                }
            }

            e.Handled = true;
        }

        private void PnlEditorFooter_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var rect = pnlEditorFooter.ClientRectangle;
            
            // "Neural Mesh Synced" Indicator
            int dotX = 350;
            int dotY = 22;
            int alpha = (int)(_pulseOpacity * 255);
            
            using (var pulseBrush = new SolidBrush(Color.FromArgb(alpha, 0, 255, 127))) // Spring Green
            {
                g.FillEllipse(pulseBrush, dotX, dotY, 8, 8);
            }
            
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using var font = new Font("Segoe UI", 7, FontStyle.Bold);
            g.DrawString("NEURAL MESH SYNCED", font, Brushes.Gray, dotX + 15, dotY - 2);
        }

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
            
            // Ensure we have a note to attach to
            if (_currentNote == null)
            {
                _currentNote = new NoteItem { CreatedAt = DateTime.Now };
            }

            // Calculate drop position if dropped on text editor
            int dropIndex = -1;
            if (sender == txtContent)
            {
                Point screenPoint = new Point(e.X, e.Y);
                Point clientPoint = txtContent.PointToClient(screenPoint);
                dropIndex = txtContent.GetCharIndexFromPosition(clientPoint);
            }
            
            string insertionText = "";
            bool addedAny = false;

            foreach (var file in files.Where(IsImageFile))
            {
                try
                {
                    var ext = Path.GetExtension(file);
                    var newName = $"{Guid.NewGuid()}{ext}";
                    var destPath = Path.Combine(_imagesFolder, newName);
                    File.Copy(file, destPath);
                    
                    _currentNote.ImagePaths.Add(destPath);
                    insertionText += $"\r\n![image]({newName})\r\n";
                    addedAny = true;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to copy image: {Path}", file);
                }
            }
            
            if (addedAny)
            {
                if (dropIndex >= 0)
                {
                    txtContent.Text = txtContent.Text.Insert(dropIndex, insertionText);
                    txtContent.SelectionStart = dropIndex + insertionText.Length;
                }
                else
                {
                    txtContent.Text += insertionText;
                }
                RefreshImages();
            }
        }

        private bool IsImageFile(string path)
        {
            var ext = Path.GetExtension(path).ToLower();
            return ext is ".png" or ".jpg" or ".jpeg" or ".gif" or ".bmp" or ".webp";
        }

        private void LblDropHint_Click(object? sender, EventArgs e)
        {
            using var openFileDialog = new OpenFileDialog
            {
                Title = "Select Images",
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.webp|All Files|*.*",
                Multiselect = true
            };
            
            if (openFileDialog.ShowDialog() != DialogResult.OK) return;
            
            // Ensure we have a note
            if (_currentNote == null)
            {
                _currentNote = new NoteItem { CreatedAt = DateTime.Now };
            }
            
            foreach (var file in openFileDialog.FileNames.Where(IsImageFile))
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
        }

        private void RefreshImages()
        {
            bool hasImages = _currentNote?.ImagePaths != null && _currentNote.ImagePaths.Any();
            
            // Only show the image panel if there are images
            pnlImages.Visible = hasImages;
            
            // Clear existing controls
            pnlImages.Controls.Clear();
            
            if (!hasImages) return;
            
            foreach (var imagePath in _currentNote.ImagePaths)
            {
                if (!File.Exists(imagePath)) continue;
                
                try
                {
                    var pic = new PictureBox
                    {
                        Size = new Size(100, 75), // Consistent 4:3 aspect ratio
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imagePath),
                        Margin = new Padding(0, 5, 10, 5), // Spacing between thumbnails
                        Cursor = Cursors.Hand,
                        Tag = imagePath,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.FromArgb(45, 45, 50)
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
        private void ApplyTheme()
        {
            Color darkBg = Color.FromArgb(20, 20, 25);
            Color panelBg = Color.FromArgb(25, 25, 30);
            Color textWhite = Color.WhiteSmoke;

            // 1. Main Background
            this.Appearance.BackColor = darkBg;
            this.Appearance.Options.UseBackColor = true;

            // 2. Split Container (if accessible)
            if (splitMain != null)
            {
                splitMain.Appearance.BackColor = darkBg;
                splitMain.Appearance.Options.UseBackColor = true;
            }

            // 3. Notes List Panel
            if (pnlNotesList != null)
            {
                pnlNotesList.LookAndFeel.UseDefaultLookAndFeel = false;
                pnlNotesList.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                pnlNotesList.Appearance.BackColor = panelBg;
                pnlNotesList.Appearance.Options.UseBackColor = true;
                pnlNotesList.AppearanceCaption.ForeColor = textWhite;
                pnlNotesList.BorderStyle = BorderStyles.NoBorder;
            }

            // 4. Note Editor Panel
            if (pnlEditor != null)
            {
                pnlEditor.LookAndFeel.UseDefaultLookAndFeel = false;
                pnlEditor.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
                pnlEditor.Appearance.BackColor = darkBg;
                pnlEditor.Appearance.Options.UseBackColor = true;
                pnlEditor.AppearanceCaption.ForeColor = textWhite;
                pnlEditor.BorderStyle = BorderStyles.NoBorder;
                pnlEditor.ShowCaption = false; 
            }

            // 5. Editors
            if (txtTitle != null)
            {
                txtTitle.Properties.Appearance.BackColor = darkBg;
                txtTitle.Properties.Appearance.ForeColor = textWhite;
                txtTitle.Properties.BorderStyle = BorderStyles.NoBorder;
            }

            if (txtContent != null)
            {
                txtContent.Properties.Appearance.BackColor = darkBg;
                txtContent.Properties.Appearance.ForeColor = textWhite;
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
                txtSearch.Properties.Appearance.BackColor = Color.FromArgb(40, 40, 45);
                txtSearch.Properties.Appearance.ForeColor = textWhite;
                txtSearch.Properties.BorderStyle = BorderStyles.Simple;
            }
        }
    }
}
