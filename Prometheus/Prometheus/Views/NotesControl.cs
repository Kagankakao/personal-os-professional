using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public NotesControl(INoteService noteService, IUserService userService)
        {
            _noteService = noteService;
            _userService = userService;
            InitializeComponent();

            this.gridViewNotes.FocusedRowChanged += GridViewNotes_FocusedRowChanged;
            this.btnNewNote.Click += BtnNewNote_Click;
            this.btnSaveNote.Click += BtnSaveNote_Click;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await RefreshNotesListAsync();
        }

        private async Task RefreshNotesListAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                _notes = await _noteService.GetNotesAsync(user.Id);
                gridControlNotes.DataSource = _notes;
                gridControlNotes.RefreshDataSource();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh notes list");
            }
        }

        private void GridViewNotes_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewNotes.GetFocusedRow() is NoteItem note)
            {
                _currentNote = note;
                txtNoteContent.Text = note.Content;
            }
        }

        private void BtnNewNote_Click(object sender, EventArgs e)
        {
            _currentNote = new NoteItem { Title = "New Note", CreatedAt = DateTime.Now };
            txtNoteContent.Text = "";
            txtNoteContent.Focus();
        }

        private async void BtnSaveNote_Click(object sender, EventArgs e)
        {
            if (_currentNote == null) _currentNote = new NoteItem { Title = "New Note", CreatedAt = DateTime.Now };

            _currentNote.Content = txtNoteContent.Text;
            
            // Extract first line as title if title is generic
            string firstLine = txtNoteContent.Lines.FirstOrDefault(l => !string.IsNullOrWhiteSpace(l)) ?? "Untitled Note";
            if (_currentNote.Title == "New Note")
            {
                _currentNote.Title = firstLine.Length > 30 ? firstLine.Substring(0, 27) + "..." : firstLine;
            }

            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                await _noteService.SaveNoteAsync(user.Id, _currentNote);
                await RefreshNotesListAsync();
                XtraMessageBox.Show("Note saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save note");
                XtraMessageBox.Show("Failed to save note: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
