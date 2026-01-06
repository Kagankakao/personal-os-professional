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
using System.IO;
using System.Linq;

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
        private bool _isLoading = true;

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

            // Setup professional appearance
            SetupUI();
            
            // Wire events
            WireEvents();

            // Initialize XP Bar Glow Timer
            InitGlowTimer();
        }

        private void SetupUI()
        {
            // Simple Timer UI
            picTimerBrand.Visible = false;
            lblTimerDisplay.Text = "25:00";
            lblTimerDisplay.Visible = true;
            lblTimerDisplay.Font = new Font("Segoe UI Light", 72F);
            lblTimerDisplay.ForeColor = Color.DimGray;

            // Setup button hover effects
            SetupButton(btnStart, Color.FromArgb(76, 175, 80));
            SetupButton(btnManualTime, Color.FromArgb(0, 188, 212));
            SetupButton(btnShowJournal, Color.FromArgb(0, 188, 212));

            // Heatmap styling
            picPixelaHeatmap.Cursor = Cursors.Hand;
            picPixelaHeatmap.Properties.NullText = " ";
            
            progressHeatmapLoading.Appearance.BackColor = Color.Transparent;
            progressHeatmapLoading.Appearance.Options.UseBackColor = true;
        }

        private void WireEvents()
        {
            btnStart.Click += BtnStart_Click;
            btnManualTime.Click += BtnManualTime_Click;
            btnShowJournal.Click += BtnShowJournal_Click;
            tmrJourneyStream.Tick += TmrJourneyStream_Tick;
            
            cboPixelaColor.SelectedIndexChanged += async (s, e) => {
                if (!_isLoading) await UpdatePixelaColorAsync();
            };

            picPixelaHeatmap.Click += async (s, e) => {
                var user = await _userService.GetCurrentUserAsync();
                if (user != null && !string.IsNullOrEmpty(user.PixelaUsername)) {
                    var url = $"https://pixe.la/v1/users/{user.PixelaUsername}/graphs/{user.PixelaGraphId}.html";
                    Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
                }
            };

        }

        private void InitGlowTimer()
        {
            tmrXPBarGlow = new System.Windows.Forms.Timer { Interval = 50 };
            tmrXPBarGlow.Tick += (s, e) => {
                if (_glowIncreasing) _glowIntensity += 5;
                else _glowIntensity -= 5;
                if (_glowIntensity >= 255) _glowIncreasing = false;
                if (_glowIntensity <= 100) _glowIncreasing = true;
                progressXP.Properties.EndColor = Color.FromArgb(255, _glowIntensity, 0);
            };
            tmrXPBarGlow.Start();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            _isLoading = true;
            try {
                await LoadPixelaColorAsync();
                await RefreshStatsAsync();
                await RefreshMotivationAsync();
                await RefreshPixelaHeatmapAsync();
                await StartJourneyStreamingAsync();
            } finally {
                _isLoading = false;
            }
        }

        private async Task LoadPixelaColorAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null) return;
            
            string color = user.PixelaGraphColor ?? "shibafu";
            string display = cboPixelaColor.Properties.Items.Cast<string>()
                .FirstOrDefault(i => i.StartsWith(color)) ?? "shibafu (Green)";
            cboPixelaColor.Text = display;
        }

        private async Task UpdatePixelaColorAsync()
        {
            try {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                string selected = cboPixelaColor.Text.Split(' ')[0];
                if (user.PixelaGraphColor == selected) return;

                user.PixelaGraphColor = selected;
                await _userService.UpdateUserAsync(user);

                var (success, error) = await _pixelaService.UpdateGraphAsync(user, color: selected);
                if (success) {
                    XtraMessageBox.Show("Theme updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _lastHeatmapRefresh = DateTime.MinValue;
                    await RefreshPixelaHeatmapAsync();
                } else {
                    XtraMessageBox.Show($"Failed: {error}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } catch (Exception ex) {
                _logger.Error(ex, "Failed to update theme");
            }
        }

        private async void BtnManualTime_Click(object? sender, EventArgs e)
        {
            try {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                using var dlg = new XtraForm();
                dlg.Text = "Log Offline Focus";
                dlg.Size = new Size(350, 240);
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.MaximizeBox = false;

                var spinHours = new SpinEdit { Location = new Point(20, 40), Size = new Size(140, 30) };
                spinHours.Properties.MinValue = 0; spinHours.Properties.MaxValue = 23;
                
                var txtNote = new TextEdit { Location = new Point(20, 100), Size = new Size(300, 30) };
                txtNote.Properties.NullText = "What did you work on?";

                var btnLog = new SimpleButton { Text = "Log Time", Location = new Point(180, 150), Size = new Size(140, 35), DialogResult = DialogResult.OK };
                dlg.Controls.AddRange(new Control[] { spinHours, txtNote, btnLog });
                
                if (dlg.ShowDialog() == DialogResult.OK) {
                    double hours = (double)spinHours.Value;
                    if (hours <= 0) return;

                    if (await _analyticsService.AddManualTimeAsync(user, hours)) {
                        using var scope = _serviceProvider.CreateScope();
                        var journal = scope.ServiceProvider.GetService<IJournalService>();
                        await journal?.AppendEntryAsync(user, txtNote.Text.Trim() ?? "Manual Log", TimeSpan.FromHours(hours));
                        
                        await RefreshStatsAsync();
                        await RefreshPixelaHeatmapAsync();
                        XtraMessageBox.Show("Logged!", "Success");
                    }
                }
            } catch (Exception ex) { _logger.Error(ex, "Manual log failed"); }
        }

        private void BtnShowJournal_Click(object? sender, EventArgs e)
        {
            if (this.ParentForm is MainDashboardForm main) main.ShowModule("Daily Journal");
        }

        private void BtnStart_Click(object? sender, EventArgs e)
        {
            string path = FindKegomoDoroPath();
            if (string.IsNullOrEmpty(path)) {
                XtraMessageBox.Show("kegomodoro/main.py not found.");
                return;
            }
            Process.Start(new ProcessStartInfo { FileName = "pythonw", Arguments = $"\"{path}\"", UseShellExecute = true, WorkingDirectory = Path.GetDirectoryName(path) });
        }

        private string FindKegomoDoroPath()
        {
            var dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            for (int i = 0; i < 7; i++) {
                if (dir == null) break;
                var p = Path.Combine(dir.FullName, "kegomodoro", "main.py");
                if (File.Exists(p)) return p;
                var p2 = Path.Combine(dir.FullName, "personal-os", "kegomodoro", "main.py");
                if (File.Exists(p2)) return p2;
                dir = dir.Parent;
            }
            return null;
        }

        private void SetupButton(SimpleButton btn, Color color)
        {
            btn.Cursor = Cursors.Hand;
            btn.AppearanceHovered.BackColor = Color.FromArgb(50, color);
            btn.AppearanceHovered.Options.UseBackColor = true;
        }

        private async Task StartJourneyStreamingAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null) return;
            
            // Get the news ticker message
            var msg = await _motivationService.GetMessageAsync(user);
            if (msg == null || string.IsNullOrEmpty(msg.Message)) return;

            _journeyText = msg.Message;

            // Ensure the stream is long enough for a smooth infinite feel
            while (_journeyText.Length < 2000)
            {
                _journeyText += "   ///   " + _journeyText;
            }
            
            // Setup label for scrolling
            lblMotivation.Text = _journeyText;
            lblMotivation.Left = pnlMotivation.Width; // Start off-screen right
            lblMotivation.Top = (pnlMotivation.Height - lblMotivation.Height) / 2;
            
            tmrJourneyStream.Interval = 30; // Faster update for smooth scroll
            tmrJourneyStream.Start();
        }

        private void TmrJourneyStream_Tick(object sender, EventArgs e)
        {
            // Move left
            lblMotivation.Left -= 2;
            
            // Reset if fully off-screen left
            if (lblMotivation.Right < 0)
            {
                lblMotivation.Left = pnlMotivation.Width;
            }
        }

        public async Task RefreshStatsAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user == null) return;
            lblLevelText.Text = $"Level {user.Level} • {user.XP % 100}/100 XP";
            progressXP.Position = (int)(user.XP % 100);
            var streak = await _analyticsService.CalculateCurrentStreakAsync(user);
            var weekly = await _analyticsService.GetWeeklyDataAsync(user, DateTime.Today);
            UpdateTile(tileItemToday, "TODAY", $"{weekly.GetValueOrDefault(DateTime.Today.DayOfWeek):F1}", "HRS");
            UpdateTile(tileItemWeek, "WEEK", $"{weekly.Values.Sum():F1}", "HRS");
            UpdateTile(tileItemTotal, "TOTAL", $"{user.TotalHours:F0}", "HRS");
            UpdateTile(tileItemStreak, "STREAK", $"{streak}", "DAYS");
        }

        private void UpdateTile(TileItem item, string title, string value, string unit)
        {
            item.Elements.Clear();
            item.Elements.Add(new TileItemElement { Text = value, TextAlignment = TileItemContentAlignment.MiddleCenter });
            item.Elements[0].Appearance.Normal.Font = new Font("Segoe UI Light", 28);
            item.Elements.Add(new TileItemElement { Text = title, TextAlignment = TileItemContentAlignment.TopLeft });
            item.Elements[1].Appearance.Normal.Font = new Font("Segoe UI", 8, FontStyle.Bold);
            item.Elements.Add(new TileItemElement { Text = unit, TextAlignment = TileItemContentAlignment.BottomRight });
        }

        private async Task RefreshPixelaHeatmapAsync()
        {
            if ((DateTime.Now - _lastHeatmapRefresh).TotalSeconds < 60 && picPixelaHeatmap.SvgImage != null) return;
            var user = await _userService.GetCurrentUserAsync();
            if (user == null) return;
            string url = $"https://pixe.la/v1/users/{user.PixelaUsername}/graphs/{user.PixelaGraphId}";
            using var client = new System.Net.Http.HttpClient();
            if (!string.IsNullOrEmpty(user.PixelaToken)) client.DefaultRequestHeaders.Add("X-USER-TOKEN", user.PixelaToken);
            var resp = await client.GetAsync(url);
            if (resp.IsSuccessStatusCode) {
                var svg = await resp.Content.ReadAsStringAsync();
                using var ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(svg));
                picPixelaHeatmap.SvgImage = DevExpress.Utils.Svg.SvgImage.FromStream(ms);
                _lastHeatmapRefresh = DateTime.Now;
            }
        }

        private async Task RefreshMotivationAsync()
        {
            var user = await _userService.GetCurrentUserAsync();
            var msg = await _motivationService.GetMessageAsync(user);
            if (msg != null) lblMotivation.Text = msg.Message;
        }
    }
}
