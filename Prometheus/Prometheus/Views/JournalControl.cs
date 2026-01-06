using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
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
        private int _hoverIndex = -1;
        private System.Windows.Forms.Timer _tmrRefresh;

        public JournalControl(IJournalService journalService, IUserService userService)
        {
            _journalService = journalService;
            _userService = userService;
            InitializeComponent();
            
            // Wire up events
            this.btnSave.Click += BtnSave_Click;
            this.btnOpenNotepad.Click += BtnOpenNotepad_Click;
            
            // Glassmorphism Paint
            this.pnlHeader.Paint += PnlHeader_Paint;
            
            // Custom Timeline Draw
            this.listEntries.DrawItem += ListEntries_DrawItem;
            this.listEntries.MouseMove += (s, e) => {
                var index = listEntries.IndexFromPoint(e.Location);
                if (index != _hoverIndex) {
                    _hoverIndex = index;
                    listEntries.Invalidate();
                }
            };
            this.listEntries.MouseLeave += (s, e) => {
                _hoverIndex = -1;
                listEntries.Invalidate();
            };

            // Refresh timer for focus metric
            _tmrRefresh = new System.Windows.Forms.Timer { Interval = 1000 };
            _tmrRefresh.Tick += async (s, e) => await UpdateFocusMetricAsync();
            _tmrRefresh.Start();
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
                
                listEntries.Items.Clear();
                foreach (var entry in _entries)
                {
                    listEntries.Items.Add(entry);
                }
                
                pnlTimeline.Text = $"Neural Echoes ({_entries.Count})";
                await UpdateFocusMetricAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh journal entries");
            }
        }

        private async Task UpdateFocusMetricAsync()
        {
            if (_currentUser == null) return;
            
            // In a real scenario, this would come from a live counter or the DB
            // For now, let's show the last entry's time if it's today
            var todayEntry = _entries.FirstOrDefault(e => e.Date.Date == DateTime.Now.Date);
            if (todayEntry != null && todayEntry.TimeWorked.HasValue)
            {
                lblFocusMetric.Text = $"FOCUS: {todayEntry.TimeWorked.Value:hh\\:mm\\:ss}";
            }
            else
            {
                lblFocusMetric.Text = "FOCUS: 00:00:00";
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

        private void PnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = pnlHeader.ClientRectangle;

            // Glass Background (Simulated Light Glass)
            using (var brush = new LinearGradientBrush(rect, 
                Color.FromArgb(180, 255, 255, 255), 
                Color.FromArgb(120, 255, 255, 255), 45f))
            {
                g.FillRectangle(brush, rect);
            }

            // Accent Line (Bottom)
            using (var pen = new Pen(Color.FromArgb(100, 255, 0, 100), 2)) // Crimson glow for Journal
            {
                g.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
            }
        }

        private void ListEntries_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= _entries.Count) return;
            
            var entry = _entries[e.Index];
            var isSelected = (e.State & DrawItemState.Selected) != 0;
            var isHovered = e.Index == _hoverIndex;
            
            // 1. Draw Background Card (Light Edition)
            var rect = e.Bounds;
            rect.Inflate(-8, -5);

            Color bgColor = Color.FromArgb(235, 235, 240);
            if (isSelected) bgColor = Color.FromArgb(60, 255, 0, 100); // Light Crimson highlight
            else if (isHovered) bgColor = Color.FromArgb(225, 225, 230);

            e.Cache.FillRectangle(bgColor, rect);

            // Selection Edge
            if (isSelected)
            {
                e.Cache.DrawLine(new Pen(Color.Crimson, 2), new Point(rect.X, rect.Y), new Point(rect.X, rect.Bottom));
            }

            // 2. Draw Text (Date)
            using (var fontDate = new Font("Segoe UI Semibold", 10))
            {
                e.Cache.DrawString(entry.Date.ToString("MMM d, yyyy"), fontDate, Brushes.Black, rect.X + 15, rect.Y + 12);
            }

            // 3. Draw Time Worked (Focus)
            if (entry.TimeWorked.HasValue)
            {
                using var fontFocus = new Font("Segoe UI", 8, FontStyle.Bold);
                e.Cache.DrawString($"FOCUS: {entry.TimeWorked.Value:hh\\:mm\\:ss}", fontFocus, Brushes.Cyan, rect.X + 15, rect.Y + 32);
            }

            // 4. Draw Note Preview
            string previewText = entry.NoteText?.Replace("\r", "").Replace("\n", " ") ?? "No thoughts recorded.";
            if (previewText.Length > 70) previewText = previewText.Substring(0, 67) + "...";

            using (var fontNote = new Font("Segoe UI", 9))
            {
                e.Cache.DrawString(previewText, fontNote, Brushes.DimGray, new Rectangle(rect.X + 15, rect.Y + 52, rect.Width - 30, 40), StringFormat.GenericDefault);
            }

            // 5. Draw Mood Badge
            if (!string.IsNullOrEmpty(entry.MoodDetected))
            {
                string mood = entry.MoodDetected;
                Color moodColor = Color.Gray;
                
                // Aesthetic color coding
                if (mood.Contains("Productive") || mood.Contains("Focused")) moodColor = Color.Cyan;
                else if (mood.Contains("Inspired") || mood.Contains("Peaceful")) moodColor = Color.Gold;
                else if (mood.Contains("Anxious") || mood.Contains("Tired")) moodColor = Color.Crimson;

                using (var fontMood = new Font("Segoe UI Semibold", 7))
                {
                    var moodSize = e.Graphics.MeasureString(mood.ToUpper(), fontMood);
                    var badgeRect = new Rectangle(rect.Right - (int)moodSize.Width - 25, rect.Y + 12, (int)moodSize.Width + 10, 18);
                    
                    e.Cache.FillRoundedRectangle(Color.FromArgb(40, moodColor), badgeRect, new DevExpress.Utils.Drawing.CornerRadius(4));
                    e.Cache.DrawString(mood.ToUpper(), fontMood, new SolidBrush(moodColor), badgeRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                }
            }

            e.Handled = true;
        }

        private void BtnOpenNotepad_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;
            _journalService.OpenInNotepad(_currentUser);
        }
    }
}
