using System;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using System.Drawing;
using System.Threading.Tasks;

namespace KeganOS.Views
{
    public partial class DashboardControl : XtraUserControl
    {
        private static DateTime _lastHeatmapRefresh = DateTime.MinValue;
        private readonly IUserService _userService;
        private readonly IMotivationalMessageService _motivationService;
        private readonly IPixelaService _pixelaService;
        private readonly IAnalyticsService _analyticsService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IKegomoDoroService _kegomoDoroService;
        private readonly ILogger _logger = Log.ForContext<DashboardControl>();
        
        private string _journeyText = "";
        private int _streamIndex = 0;
        private System.Windows.Forms.Timer tmrXPBarGlow;
        private int _glowIntensity = 0;
        private bool _glowIncreasing = true;

        public DashboardControl(IUserService userService, IMotivationalMessageService motivationService, 
            IPixelaService pixelaService, IAnalyticsService analyticsService, 
            IKegomoDoroService kegomoDoroService, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _motivationService = motivationService;
            _pixelaService = pixelaService;
            _analyticsService = analyticsService;
            _kegomoDoroService = kegomoDoroService;
            _serviceProvider = serviceProvider;
            InitializeComponent();

            // Simple Timer UI (Requested)
            picTimerBrand.Visible = false;
            lblTimerDisplay.Text = "25:00";
            lblTimerDisplay.Visible = true;
            lblTimerDisplay.Font = new System.Drawing.Font("Segoe UI Light", 72F);
            lblTimerDisplay.ForeColor = System.Drawing.Color.DimGray; // Professional grey
            
            // Setup simple button hover effects
            SetupButton(btnStart, System.Drawing.Color.FromArgb(76, 175, 80)); // Green
            SetupButton(btnCustomize, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan
            SetupButton(btnLaunchKegomoDoro, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan
            SetupButton(btnManualTime, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan
            SetupButton(btnShowJournal, System.Drawing.Color.FromArgb(0, 188, 212)); // Cyan

            // Setup main buttons
            btnStart.Click += BtnStart_Click;
            btnCustomize.Click += BtnCustomize_Click;
            btnLaunchKegomoDoro.Visible = false; // Hidden as requested, integrated into Start
            
            btnManualTime.Click += BtnManualTime_Click;
            btnShowJournal.Click += BtnShowJournal_Click;

            // Setup heatmap cursor and click
            picPixelaHeatmap.Cursor = System.Windows.Forms.Cursors.Hand;
            picPixelaHeatmap.Properties.NullText = " ";
            
            // Setup Journey Streamer
            tmrJourneyStream.Tick += TmrJourneyStream_Tick;
            
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

            // Ensure transparency
            progressHeatmapLoading.Appearance.BackColor = System.Drawing.Color.Transparent;
            progressHeatmapLoading.Appearance.Options.UseBackColor = true;

            // Initialize XP Bar Glow Timer
            tmrXPBarGlow = new System.Windows.Forms.Timer();
            tmrXPBarGlow.Interval = 50;
            tmrXPBarGlow.Tick += (s, e) => {
                if (_glowIncreasing) _glowIntensity += 5;
                else _glowIntensity -= 5;

                if (_glowIntensity >= 255) _glowIncreasing = false;
                if (_glowIntensity <= 100) _glowIncreasing = true;

                progressXP.Properties.EndColor = Color.FromArgb(255, _glowIntensity, 0);
            };
            tmrXPBarGlow.Start();
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            try
            {
                string kegomoDoroPath = FindKegomoDoroPath();
                
                if (!string.IsNullOrEmpty(kegomoDoroPath) && System.IO.File.Exists(kegomoDoroPath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "pythonw",
                        Arguments = $"\"{kegomoDoroPath}\"",
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = System.IO.Path.GetDirectoryName(kegomoDoroPath)
                    });
                    _logger.Information("Launched KegomoDoro from {Path}", kegomoDoroPath);
                }
                else
                {
                    XtraMessageBox.Show("KegomoDoro 'main.py' not found in parent directories.", "Error", 
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

        private async void BtnManualTime_Click(object? sender, EventArgs e)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                // Create professional dialog programmatically
                using var dlg = new XtraForm();
                dlg.Text = "Log Offline Focus";
                dlg.Size = new System.Drawing.Size(350, 240);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;
                dlg.MinimizeBox = false;

                // Controls
                var lblHours = new LabelControl { Text = "Hours", Location = new System.Drawing.Point(20, 20) };
                var spinHours = new SpinEdit { Location = new System.Drawing.Point(20, 40), Size = new System.Drawing.Size(140, 30) };
                spinHours.Properties.MinValue = 0;
                spinHours.Properties.MaxValue = 23;
                spinHours.Value = 1;

                var lblMinutes = new LabelControl { Text = "Minutes", Location = new System.Drawing.Point(180, 20) };
                var spinMinutes = new SpinEdit { Location = new System.Drawing.Point(180, 40), Size = new System.Drawing.Size(140, 30) };
                spinMinutes.Properties.MinValue = 0;
                spinMinutes.Properties.MaxValue = 59;
                spinMinutes.Value = 0;

                var lblNote = new LabelControl { Text = "Activity Note (Optional)", Location = new System.Drawing.Point(20, 80) };
                var txtNote = new TextEdit { Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(300, 30) };
                txtNote.Properties.NullText = "E.g. Reading, Deep Work...";

                var btnSave = new SimpleButton { Text = "Log Time", Location = new System.Drawing.Point(180, 150), Size = new System.Drawing.Size(140, 35) };
                btnSave.DialogResult = DialogResult.OK;
                // Style the save button
                btnSave.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                btnSave.Appearance.ForeColor = Color.FromArgb(76, 175, 80); // Green
                
                var btnCancel = new SimpleButton { Text = "Cancel", Location = new System.Drawing.Point(20, 150), Size = new System.Drawing.Size(140, 35) };
                btnCancel.DialogResult = DialogResult.Cancel;

                dlg.Controls.AddRange(new Control[] { lblHours, spinHours, lblMinutes, spinMinutes, lblNote, txtNote, btnSave, btnCancel });
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    double totalHours = (double)spinHours.Value + ((double)spinMinutes.Value / 60.0);
                    
                    if (totalHours <= 0)
                    {
                        XtraMessageBox.Show("Time must be greater than zero.", "Invalid Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 1. Update Stats
                    bool success = await _analyticsService.AddManualTimeAsync(user, totalHours);
                    
                    // 2. Log to Journal (Best Practice)
                    try 
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var journalService = scope.ServiceProvider.GetService<IJournalService>();
                        if (journalService != null)
                        {
                            string note = txtNote.Text.Trim();
                            if (string.IsNullOrEmpty(note)) note = "Manual Offline Focus";
                            await journalService.AppendEntryAsync(user, $"[Manual Log] {note}", TimeSpan.FromHours(totalHours));
                        }
                    }
                    catch (Exception jEx) 
                    {
                        _logger.Warning(jEx, "Failed to append manual log to text journal");
                    }

                    if (success)
                    {
                        XtraMessageBox.Show($"Successfully logged {totalHours:F2} hours!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await RefreshStatsAsync();
                        await RefreshPixelaHeatmapAsync();
                    }
                    else
                    {
                        XtraMessageBox.Show("Failed to log time. Check logs or internet connection.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to log manual time");
            }
        }

        private async void BtnShowJournal_Click(object? sender, EventArgs e)
        {
            try
            {
                // Resolve JournalControl via ServiceProvider
                using var scope = _serviceProvider.CreateScope(); 
                var journalControl = (XtraUserControl)scope.ServiceProvider.GetService(typeof(JournalControl));
                if (journalControl == null)
                {
                    XtraMessageBox.Show("Journal module not loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var form = new XtraForm();
                form.Text = "Daily Journal";
                form.Size = new System.Drawing.Size(800, 600);
                form.StartPosition = FormStartPosition.CenterParent;
                form.Controls.Add(journalControl);
                journalControl.Dock = DockStyle.Fill;
                
                form.ShowDialog();
                // Refresh stats after closing logic if needed
                await RefreshStatsAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to open journal");
            }
        }

        private void BtnCustomize_Click(object? sender, EventArgs e)
        {
            try
            {
                 // Resolve SettingsControl
                var settingsControl = (XtraUserControl)_serviceProvider.GetService(typeof(SettingsControl));
                
                if (settingsControl == null)
                {
                     XtraMessageBox.Show("Settings module not loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     return;
                }

                using var settingsForm = new XtraForm();
                settingsForm.Text = "Settings";
                settingsForm.Size = new System.Drawing.Size(600, 500);
                settingsForm.StartPosition = FormStartPosition.CenterParent;
                settingsForm.Controls.Add(settingsControl);
                settingsControl.Dock = DockStyle.Fill;

                settingsForm.ShowDialog();
                // Refresh anything if needed
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to open settings");
            }
        }

        private string FindKegomoDoroPath()
        {
            var currentDir = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            // Search up to 6 levels up
            for (int i = 0; i < 7; i++)
            {
                if (currentDir == null) break;
                
                var potentialPath = System.IO.Path.Combine(currentDir.FullName, "kegomodoro", "main.py");
                if (System.IO.File.Exists(potentialPath))
                {
                    return potentialPath;
                }
                
                // Also check nested personal-os structure
                var potentialPathNested = System.IO.Path.Combine(currentDir.FullName, "personal-os", "kegomodoro", "main.py");
                if (System.IO.File.Exists(potentialPathNested))
                {
                    return potentialPathNested;
                }

                currentDir = currentDir.Parent;
            }
            return null;
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
            await LoadBrandingImageAsync();
            await StartJourneyStreamingAsync();

            // Glassmorphism logic for Motivation Panel
            pnlMotivation.Paint += (s, pe) => {
                var g = pe.Graphics;
                var rect = pnlMotivation.ClientRectangle;
                rect.Height = 40; // Header area
                
                // Semi-transparent glass background
                using var brush = new SolidBrush(Color.FromArgb(40, 255, 255, 255));
                g.FillRectangle(brush, rect);
                
                // Bottom border line for the header
                using var pen = new Pen(Color.FromArgb(60, 255, 255, 255), 1);
                g.DrawLine(pen, 0, 39, pnlMotivation.Width, 39);
                
                // Title
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                using var titleFont = new Font("Segoe UI Semibold", 9);
                g.DrawString("KEGANOS • PERSONAL JOURNEY STREAM", titleFont, Brushes.Cyan, new PointF(15, 12));

                // Mental Health Indicator (Right aligned)
                var moodRect = new Rectangle(pnlMotivation.Width - 180, 15, 120, 10);
                g.FillRectangle(new SolidBrush(Color.FromArgb(50, 0, 0, 0)), moodRect);
                
                var moodValRect = new Rectangle(moodRect.X, moodRect.Y, 90, 10); // 75% mood
                using var moodFill = new System.Drawing.Drawing2D.LinearGradientBrush(moodValRect, Color.Lime, Color.SpringGreen, 0f);
                g.FillRectangle(moodFill, moodValRect);

                g.DrawString("MOOD: STABLE", new Font("Segoe UI", 7, FontStyle.Bold), Brushes.White, new PointF(moodRect.Right + 5, 12));
            };
        }

        private async System.Threading.Tasks.Task LoadBrandingImageAsync()
        {
            try
            {
                // Find main_image.png in kegomodoro
                string baseDir = FindKegomoDoroPath();
                if (!string.IsNullOrEmpty(baseDir))
                {
                    string imagesDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(baseDir), "dependencies", "images");
                    string brandImgPath = System.IO.Path.Combine(imagesDir, "main_image.png");

                    if (System.IO.File.Exists(brandImgPath))
                    {
                        picTimerBrand.Image = System.Drawing.Image.FromFile(brandImgPath);
                        _logger.Debug("Loaded branding image from {Path}", brandImgPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Failed to load branding image");
            }
        }

        private async System.Threading.Tasks.Task StartJourneyStreamingAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                // Resolve journey path: kegomodoro/dependencies/texts/Users/[Username]/[JournalFileName]
                string kegMain = FindKegomoDoroPath();
                if (string.IsNullOrEmpty(kegMain)) return;

                string userDir = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(kegMain), "dependencies", "texts", "Users", user.DisplayName);
                string journalFile = user.JournalFileName ?? "KAÆ[Æß#.txt";
                string fullPath = System.IO.Path.Combine(userDir, journalFile);

                if (System.IO.File.Exists(fullPath))
                {
                    _journeyText = await System.IO.File.ReadAllTextAsync(fullPath);
                    if (!string.IsNullOrWhiteSpace(_journeyText))
                    {
                        _streamIndex = 0;
                        lblMotivation.Text = "";
                        tmrJourneyStream.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to start journey streaming");
            }
        }

        private void TmrJourneyStream_Tick(object sender, EventArgs e)
        {
            if (_streamIndex < _journeyText.Length)
            {
                lblMotivation.Text += _journeyText[_streamIndex];
                _streamIndex++;
            }
            else
            {
                tmrJourneyStream.Stop();
            }
        }

        public async Task RefreshStatsAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                // Level & XP
                lblLevelText.Text = $"Level {user.Level} • {user.XP % 100}/100 XP";
                progressXP.Position = (int)(user.XP % 100);
                progressXP.Properties.StartColor = Color.FromArgb(192, 0, 0); // Imperial Crimson
                progressXP.Properties.EndColor = Color.FromArgb(255, 69, 0);

                // Fetch latest data (Streak & Weekly)
                var streak = await _analyticsService.CalculateCurrentStreakAsync(user);
                var weeklyData = await _analyticsService.GetWeeklyDataAsync(user, DateTime.Today);
                
                var todayHours = weeklyData.ContainsKey(DateTime.Today.DayOfWeek) ? weeklyData[DateTime.Today.DayOfWeek] : 0;
                var weekHours = weeklyData.Values.Sum();

                // Update Tiles
                UpdateTile(tileItemToday, "TODAY", $"{todayHours:F1}", "HRS");
                UpdateTile(tileItemWeek, "WEEK", $"{weekHours:F1}", "HRS");
                UpdateTile(tileItemTotal, "TOTAL", $"{user.TotalHours:F0}", "HRS");
                UpdateTile(tileItemStreak, "STREAK", $"{streak}", "DAYS");
                
                _logger.Debug("Stats refreshed for user {User}", user.DisplayName);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to refresh stats");
            }
        }

        private void UpdateTile(TileItem item, string title, string value, string unit)
        {
            item.Elements.Clear();
            
            // Value
            var elValue = new TileItemElement { Text = value, TextAlignment = TileItemContentAlignment.MiddleCenter };
            elValue.Appearance.Normal.Font = new Font("Segoe UI Light", 28);
            item.Elements.Add(elValue);

            // Title
            var elTitle = new TileItemElement { Text = title, TextAlignment = TileItemContentAlignment.TopLeft };
            elTitle.Appearance.Normal.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            elTitle.Appearance.Normal.ForeColor = Color.FromArgb(200, 255, 255, 255);
            item.Elements.Add(elTitle);

            // Unit
            var elUnit = new TileItemElement { Text = unit, TextAlignment = TileItemContentAlignment.BottomRight };
            elUnit.Appearance.Normal.Font = new Font("Segoe UI", 8, FontStyle.Italic);
            elUnit.Appearance.Normal.ForeColor = Color.FromArgb(150, 255, 255, 255);
            item.Elements.Add(elUnit);
        }

        private async System.Threading.Tasks.Task RefreshPixelaHeatmapAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                // Don't refresh if we already refreshed in the last 60 seconds
                var timeSinceLastRefresh = DateTime.Now - _lastHeatmapRefresh;
                
                if (timeSinceLastRefresh.TotalSeconds < 60 && picPixelaHeatmap.SvgImage != null)
                {
                    _logger.Debug("Skipping heatmap refresh, too soon. Using cached view.");
                    picPixelaHeatmap.Visible = true;
                    return;
                }

                // Show spinner only if we don't have an image yet
                if (picPixelaHeatmap.SvgImage == null)
                {
                    progressHeatmapLoading.Visible = true;
                }
                
                // Pixe.la returns SVG by default.
                // Added a cache-buster query parameter to force fresh data.
                string url = $"https://pixe.la/v1/users/{user.PixelaUsername}/graphs/{user.PixelaGraphId}?t={DateTime.Now.Ticks}";
                
                _logger.Debug("Fetching Pixe.la heatmap (SVG) from {Url}", url);

                string cachePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pixela_cache.svg");

                // 1. Load from cache immediately if exists
                if (System.IO.File.Exists(cachePath))
                {
                    try
                    {
                         _logger.Debug("Loading Pixe.la heatmap from cache: {Path}", cachePath);
                         using var fs = System.IO.File.OpenRead(cachePath);
                         var cachedSvg = DevExpress.Utils.Svg.SvgImage.FromStream(fs);
                         picPixelaHeatmap.Image = null;
                         picPixelaHeatmap.SvgImage = cachedSvg;
                         picPixelaHeatmap.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warning(ex, "Failed to load cached heatmap");
                    }
                }

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
                            // 1. Strip the background rectangle if present (Pixe.la puts a white rect at the start)
                            var rectStart = svgContent.IndexOf("<rect");
                            if (rectStart > 0)
                            {
                                var rectEnd = svgContent.IndexOf("/>", rectStart);
                                if (rectEnd > rectStart && svgContent.Substring(rectStart, rectEnd - rectStart).Contains("white"))
                                {
                                    svgContent = svgContent.Remove(rectStart, (rectEnd + 2) - rectStart);
                                }
                            }

                            // 2. Normalize colors (ensure white text/lines match the theme if needed)
                            svgContent = svgContent.Replace("fill=\"white\"", "fill=\"#FFFFFF\"")
                                                   .Replace("fill-opacity=\"0.5\"", "fill-opacity=\"1.0\"");

                            using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(svgContent));
                            
                            var svgImage = DevExpress.Utils.Svg.SvgImage.FromStream(ms);
                            
                            // Clear potential legacy Image property to avoid conflicts
                            picPixelaHeatmap.Image = null; 
                            picPixelaHeatmap.SvgImage = svgImage;
                            picPixelaHeatmap.Visible = true;
                            
                            _logger.Information("Pixe.la heatmap (SVG) refreshed successfully. Content Length: {Length}", svgContent.Length);
                            
                            // Update cache
                            try
                            {
                                await System.IO.File.WriteAllTextAsync(cachePath, svgContent);
                                _lastHeatmapRefresh = DateTime.Now;
                                _logger.Debug("Updated Pixe.la heatmap cache");
                            }
                            catch (Exception ex)
                            {
                                _logger.Warning(ex, "Failed to write heatmap cache");
                            }

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
                // picPixelaHeatmap.Visible = false; // Don't hide on error if we already have a cached one
            }
            finally
            {
                progressHeatmapLoading.Visible = false;
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
                lblMotivation.Text = "Embark on your journey...";
            }
        }

        private void BtnLaunchKegomoDoro_Click(object? sender, EventArgs e)
        {
            try
            {
                _logger.Information("Manual launch of KEGOMODORO requested");
                _kegomoDoroService.Launch();
                
                if (!string.IsNullOrEmpty(_kegomoDoroService.LastError))
                {
                    XtraMessageBox.Show(_kegomoDoroService.LastError, "Launch Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to launch KegomoDoro manually");
                XtraMessageBox.Show($"Failed to launch: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
