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
        private readonly IPixelaService _pixelaService;
        private readonly IAnalyticsService _analyticsService;
        private readonly ILogger _logger = Log.ForContext<DashboardControl>();

        public DashboardControl(IUserService userService, IMotivationalMessageService motivationService, IPixelaService pixelaService, IAnalyticsService analyticsService)
        {
            _userService = userService;
            _motivationService = motivationService;
            _pixelaService = pixelaService;
            _analyticsService = analyticsService;
            InitializeComponent();

            // Setup simple button hover effects
            SetupButton(btnStart, System.Drawing.Color.FromArgb(76, 175, 80)); // Green
            SetupButton(btnCustomize, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan
            SetupButton(btnLaunchKegomoDoro, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan

            // Setup heatmap cursor and click
            picPixelaHeatmap.Cursor = System.Windows.Forms.Cursors.Hand;
            picPixelaHeatmap.Click += async (s, e) => 
            {
                var currentUser = await _userService.GetCurrentUserAsync();
                if (currentUser is not null && !string.IsNullOrEmpty(currentUser.PixelaUsername) && !string.IsNullOrEmpty(currentUser.PixelaGraphId))
                {
                    var graphUrl = $"https://pixe.la/v1/users/{currentUser.PixelaUsername}/graphs/{currentUser.PixelaGraphId}.html";
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo 
                    { 
                        FileName = graphUrl, 
                        UseShellExecute = true 
                    });
                }
            };
        }

        private void SetupButton(DevExpress.XtraEditors.SimpleButton btn, System.Drawing.Color baseColor)
        {
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
            btn.AppearanceHovered.BackColor = System.Drawing.Color.FromArgb(50, baseColor);
            btn.AppearanceHovered.Options.UseBackColor = true;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            await RefreshStatsAsync();
            await RefreshMotivationAsync();
            await RefreshPixelaHeatmapAsync();
        }

        public async System.Threading.Tasks.Task RefreshStatsAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user != null)
                {
                    // Level & XP
                    lblLevelText.Text = $"Level {user.Level} • {user.XpInCurrentLevel}/{user.XpRequiredForLevel} XP";
                    progressXP.Position = (int)(user.LevelProgress * 100);
                    lblLevelText.Appearance.ForeColor = System.Drawing.ColorTranslator.FromHtml(user.DisplayLevelColor);

                    // Today/Week Stats from Analytics
                    var weeklyData = await _analyticsService.GetWeeklyDataAsync(user, DateTime.Today);
                    double weekTotal = 0;
                    double todayTotal = 0;
                    
                    foreach (var kvp in weeklyData)
                    {
                        weekTotal += kvp.Value;
                        if (kvp.Key == DateTime.Today.DayOfWeek)
                        {
                            todayTotal = kvp.Value;
                        }
                    }

                    lblTodayValue.Text = $"{todayTotal:F1} hrs";
                    lblWeekValue.Text = $"{weekTotal:F1} hrs";
                    lblTotalValue.Text = $"{user.TotalHours:F1} hrs";
                    
                    // Streak
                    int streak = await _analyticsService.CalculateCurrentStreakAsync(user);
                    lblStreakValue.Text = $"{streak} days";
                    lblBestValue.Text = $"{Math.Max(streak, user.BestStreak)} days";
                    
                    _logger.Debug("Stats refreshed for user {User}", user.DisplayName);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh stats");
            }
        }

        private async System.Threading.Tasks.Task RefreshPixelaHeatmapAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null || !_pixelaService.IsConfigured(user))
                {
                    picPixelaHeatmap.Visible = false;
                    return;
                }

                picPixelaHeatmap.Visible = true;
                
                // Pixe.la returns SVG by default.
                // User requested WHITE background, so we remove appearance=dark and don't strip the background.
                string url = $"https://pixe.la/v1/users/{user.PixelaUsername}/graphs/{user.PixelaGraphId}";
                
                _logger.Debug("Fetching Pixe.la heatmap (SVG) from {Url}", url);

                using var client = new System.Net.Http.HttpClient();
                if (!string.IsNullOrEmpty(user.PixelaToken))
                {
                    client.DefaultRequestHeaders.Add("X-USER-TOKEN", user.PixelaToken);
                }

                // Retry loop for 429/503 errors (Pixe.la free tier rate limiting)
                int maxRetries = 3;
                bool success = false;
                
                for (int i = 0; i < maxRetries; i++)
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var svgContent = await response.Content.ReadAsStringAsync();
                        try 
                        {
                            // Load as SvgImage
                            // Normalize SVG content for DevExpress compatibility
                            // strict replacement of known issues
                            svgContent = svgContent.Replace("fill=\"white\"", "fill=\"#FFFFFF\"")
                                                   .Replace("fill-opacity=\"0.5\"", "fill-opacity=\"1.0\"");

                            using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgContent));
                            
                            var svgImage = DevExpress.Utils.Svg.SvgImage.FromStream(ms);
                            
                            // Clear potential legacy Image property to avoid conflicts
                            picPixelaHeatmap.Image = null; 
                            picPixelaHeatmap.SvgImage = svgImage;
                            picPixelaHeatmap.Visible = true;
                            
                            _logger.Information("Pixe.la heatmap (SVG) refreshed successfully. Content Length: {Length}", svgContent.Length);
                            success = true;
                            break; // Success
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, "Failed to parse SVG content. Content start: {Content}", svgContent.Substring(0, Math.Min(100, svgContent.Length)));
                            picPixelaHeatmap.Visible = false;
                            break; // Fatal parsing error
                        }
                    }
                    else
                    {
                        var errorCode = (int)response.StatusCode;
                        if (errorCode == 429 || errorCode == 503 || response.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                        {
                            _logger.Warning("Pixe.la rate limited (Attempt {Attempt}/{Max}). Retrying...", i + 1, maxRetries);
                            await System.Threading.Tasks.Task.Delay(1000 * (i + 1)); // Backoff
                            continue;
                        }
                        
                        var errorBody = await response.Content.ReadAsStringAsync();
                        _logger.Warning("Pixe.la API returned {Status} for heatmap: {Body}", response.StatusCode, errorBody);
                        picPixelaHeatmap.Visible = false;
                        break; // Fatal error
                    }
                }
                
                if (!success)
                {
                    picPixelaHeatmap.Visible = false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh Pixela heatmap");
                picPixelaHeatmap.Visible = false;
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
