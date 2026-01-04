using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
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

        public NotesControl(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
            
            // Setup images folder
            _imagesFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "KeganOS", "NoteImages");
            Directory.CreateDirectory(_imagesFolder);
            
            InitializeComponent();

            // Wire up events
            this.listNotes.SelectedIndexChanged += ListNotes_SelectedIndexChanged;
            this.btnNewNote.Click += BtnNewNote_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.txtContent.EditValueChanged += TxtContent_EditValueChanged;
            this.txtSearch.EditValueChanged += TxtSearch_EditValueChanged;
            
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
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await RefreshNotesListAsync();
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
                _filteredNotes = _notes
                    .Where(n => n.Title.ToLower().Contains(filter) || 
                                (n.Content?.ToLower().Contains(filter) ?? false))
                    .OrderByDescending(n => n.LastModified)
                    .ToList();
            }
            
            // Update listbox with formatted items
            listNotes.Items.Clear();
            foreach (var note in _filteredNotes)
            {
                string date = note.LastModified.ToString("MMM d");
                string preview = string.IsNullOrEmpty(note.Content) ? "" : 
                    (note.Content.Length > 40 ? note.Content.Substring(0, 37) + "..." : note.Content);
                listNotes.Items.Add($"{note.Title}\n{date} • {preview}");
            }
            
            pnlNotesList.Text = $"Notes ({_filteredNotes.Count})";
        }

        private void TxtSearch_EditValueChanged(object? sender, EventArgs e)
        {
            ApplyFilter();
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
                
                // Ensure editor is ready and focused
                txtContent.Focus();
                if (txtContent.Text.Length > 0)
                    txtContent.SelectionStart = txtContent.Text.Length;
            }
        }

        private void BtnNewNote_Click(object? sender, EventArgs e)
        {
            _currentNote = new NoteItem 
            { 
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
            if (_currentNote == null) 
            {
                _currentNote = new NoteItem { CreatedAt = DateTime.Now };
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
                
                // Show subtle feedback
                lblLastSaved.Text = "✓ Saved just now";
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save note");
                XtraMessageBox.Show("Failed to save note: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void TxtContent_EditValueChanged(object? sender, EventArgs e)
        {
            UpdateWordCount();
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
    }
}
