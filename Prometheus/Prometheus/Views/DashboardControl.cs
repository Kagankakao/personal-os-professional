using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;

namespace KeganOS.Views
{
    public partial class DashboardControl : XtraUserControl
    {
        private readonly IUserService _userService;
        private readonly IMotivationalMessageService _motivationService;
        private readonly ILogger _logger = Log.ForContext<DashboardControl>();

        public DashboardControl(IUserService userService, IMotivationalMessageService motivationService)
        {
            _userService = userService;
            _motivationService = motivationService;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            await RefreshStatsAsync();
            await RefreshMotivationAsync();
        }

        public async System.Threading.Tasks.Task RefreshStatsAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user != null)
                {
                    // Today/Week/Total stats
                    lblTodayValue.Text = "0 hrs"; // Would need analytics service
                    lblWeekValue.Text = "0 hrs";
                    lblTotalValue.Text = $"{user.TotalHours:F0} hrs";
                    
                    // Level & XP
                    lblLevelText.Text = $"Level {user.Level} • {user.XpInCurrentLevel}/{user.XpRequiredForLevel} XP";
                    progressXP.Position = (int)(user.LevelProgress * 100);
                    lblLevelText.Appearance.ForeColor = System.Drawing.ColorTranslator.FromHtml(user.DisplayLevelColor);
                    
                    // Streak
                    lblStreakValue.Text = "0 days"; // Would need analytics
                    lblBestValue.Text = $"{user.BestStreak} days";
                    
                    _logger.Debug("Stats refreshed for user {User}", user.DisplayName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh stats");
            }
        }

        private async System.Threading.Tasks.Task RefreshMotivationAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                var message = await _motivationService.GetMessageAsync(user);
                if (message != null)
                {
                    lblMotivation.Text = message.Message;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh motivation");
            }
        }

        private void BtnLaunchKegomoDoro_Click(object? sender, EventArgs e)
        {
            try
            {
                var kegomoDoroPath = System.IO.Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    "..", "..", "..", "..", "kegomodoro", "main.py");
                
                if (System.IO.File.Exists(kegomoDoroPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "python",
                        Arguments = $"\"{kegomoDoroPath}\"",
                        UseShellExecute = true,
                        WorkingDirectory = System.IO.Path.GetDirectoryName(kegomoDoroPath)
                    });
                }
                else
                {
                    XtraMessageBox.Show("KegomoDoro not found at expected path.", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to launch KegomoDoro");
                XtraMessageBox.Show($"Failed to launch: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
