using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using DevExpress.XtraCharts;

namespace KeganOS.Views
{
    public partial class AnalyticsControl : XtraUserControl
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<AnalyticsControl>();

        public AnalyticsControl(IAnalyticsService analyticsService, IUserService userService)
        {
            _analyticsService = analyticsService;
            _userService = userService;
            InitializeComponent();
            
            // Wire up glassmorphism paint
            // Wire up glassmorphism paint
            this.pnlHeader.Paint += PnlHeader_Paint;
            
            if (this.btnConsultAI != null)
                this.btnConsultAI.Click += BtnConsultAI_Click;
        }

        private async void BtnConsultAI_Click(object? sender, EventArgs e)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;
                
                btnConsultAI.Enabled = false;
                btnConsultAI.Text = "Generating...";

                var report = await _analyticsService.GenerateInsightAsync(user, DateTime.Today, "WeeklyReport");
                
                // Show in a nice dialog
                using var dlg = new XtraForm();
                dlg.Text = "Prometheus Weekly Report";
                dlg.Size = new Size(500, 600);
                dlg.StartPosition = FormStartPosition.CenterParent;
                
                var memo = new MemoEdit();
                memo.Dock = DockStyle.Fill;
                memo.Properties.ReadOnly = true;
                memo.Text = report;
                memo.Properties.Appearance.Font = new Font("Segoe UI", 10);
                
                dlg.Controls.Add(memo);
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to consult AI");
                XtraMessageBox.Show("Failed to consult AI: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (btnConsultAI != null)
                {
                    btnConsultAI.Enabled = true;
                    btnConsultAI.Text = "Consult AI";
                }
            }
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                // 1. Summary Stats
                lblTotalHours.Text = $"Total Focus: {user.TotalHours:F1} hours";
                
                var streak = await _analyticsService.CalculateCurrentStreakAsync(user);
                lblTotalSessions.Text = $"Current Streak: {streak} days";
                
                lblAverageSession.Text = $"Best Streak: {user.BestStreak} days";

                // 2. Weekly Chart
                DateTime monday = DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);
                var weeklyData = await _analyticsService.GetWeeklyDataAsync(user, monday);
                
                chartWeeklyProgress.Series.Clear();
                Series series = new Series("Focus Evolution", ViewType.Bar);
                
                foreach (var kvp in weeklyData.OrderBy(x => x.Key))
                {
                    series.Points.Add(new SeriesPoint(kvp.Key.ToString(), kvp.Value));
                }

                chartWeeklyProgress.Series.Add(series);

                // Styling
                if (series.View is AreaSeriesView view)
                {
                    view.Transparency = 150;
                    view.Color = Color.DarkGreen;
                    view.Border.Visibility = DevExpress.Utils.DefaultBoolean.True;
                }

                chartWeeklyProgress.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False;
                XYDiagram diagram = chartWeeklyProgress.Diagram as XYDiagram;
                if (diagram != null)
                {
                    diagram.AxisY.Title.Text = "Hours";
                    diagram.AxisY.Title.Visibility = DevExpress.Utils.DefaultBoolean.True;
                    diagram.AxisX.Label.Angle = -45;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load analytics data");
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

            // Accent Line (Bottom) - Gold for Analytics
            using (var pen = new Pen(Color.FromArgb(100, 255, 200, 0), 2))
            {
                g.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
            }
        }
    }
}
