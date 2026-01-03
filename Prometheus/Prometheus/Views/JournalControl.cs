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
    public partial class JournalControl : XtraUserControl
    {
        private readonly IJournalService _journalService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<JournalControl>();
        private User? _currentUser;
        private List<JournalEntry> _entries = new();

        public JournalControl(IJournalService journalService, IUserService userService)
        {
            _journalService = journalService;
            _userService = userService;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            try
            {
                _currentUser = await _userService.GetCurrentUserAsync();
                await RefreshEntriesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to initialize JournalControl");
            }
        }

        private async Task RefreshEntriesAsync()
        {
            if (_currentUser == null) return;

            try
            {
                var entries = await _journalService.ReadEntriesAsync(_currentUser);
                _entries = entries.OrderByDescending(e => e.Date).ToList();
                gridControlEntries.DataSource = _entries;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh journal entries");
            }
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;
            
            string note = txtEntry.Text.Trim();
            if (string.IsNullOrEmpty(note))
            {
                XtraMessageBox.Show("Please enter a note before saving.", "Journal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Note-only append (doesn't modify time worked)
                await _journalService.AppendNoteOnlyAsync(_currentUser, note);
                txtEntry.Text = string.Empty;
                
                XtraMessageBox.Show("Entry added to your journal!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await RefreshEntriesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save journal entry");
                XtraMessageBox.Show("Failed to save entry: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenNotepad_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;
            _journalService.OpenInNotepad(_currentUser);
        }

        private void GridViewEntries_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewEntries.GetFocusedRow() is JournalEntry entry)
            {
                // We could show the full text in a preview, but the grid shows NoteText
                // For now, let's keep it simple.
            }
        }
    }
}
