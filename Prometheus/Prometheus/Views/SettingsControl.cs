using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
    public partial class SettingsControl : XtraUserControl
    {
        private readonly IUserService _userService;
        private readonly IKegomoDoroService _kegomoDoroService;
        private readonly IBackupService _backupService;
        private readonly IPrometheusService _prometheusService;
        private readonly IAIProvider _aiProvider;
        private readonly ILogger _logger = Log.ForContext<SettingsControl>();
        private UserPreferences? _preferences;
        private User? _currentUser;

        // Event for logout
        public event EventHandler? LogoutRequested;

        public SettingsControl(IUserService userService, IKegomoDoroService kegomoDoroService, 
            IBackupService backupService, IPrometheusService prometheusService, IAIProvider aiProvider)
        {
            _userService = userService;
            _kegomoDoroService = kegomoDoroService;
            _backupService = backupService;
            _prometheusService = prometheusService;
            _aiProvider = aiProvider;
            InitializeComponent();

            this.btnSaveSettings.Click += BtnSaveSettings_Click;
            this.btnCreateBackup.Click += BtnCreateBackup_Click;
            this.btnRestoreBackup.Click += BtnRestoreBackup_Click;
            this.btnLogout.Click += BtnLogout_Click;
            this.btnShowCredentials.Click += BtnShowCredentials_Click;
            this.btnOpenBackupFolder.Click += BtnOpenBackupFolder_Click;
            this.pnlHeader.Paint += PnlHeader_Paint;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await LoadSettingsAsync();
        }

        private async Task LoadSettingsAsync()
        {
            try
            {
                _currentUser = await _userService.GetCurrentUserAsync();
                if (_currentUser == null) return;
                
                _preferences = await _kegomoDoroService.GetConfigurationAsync(_currentUser);

                // KegomoDoro settings
                if (_preferences != null)
                {
                    spinWorkMin.Value = _preferences.KegomoDoroWorkMin;
                    spinShortBreak.Value = _preferences.KegomoDoroShortBreak;
                    spinLongBreak.Value = _preferences.KegomoDoroLongBreak;
                    
                    if (!string.IsNullOrEmpty(_preferences.KegomoDoroBackgroundColor))
                    {
                        colorPickBack.Color = ColorTranslator.FromHtml(_preferences.KegomoDoroBackgroundColor);
                    }
                }
                
                // API Keys
                txtGeminiKey.Text = _currentUser.GeminiApiKey ?? "";
                txtPixelaUser.Text = _currentUser.PixelaUsername ?? "";
                txtPixelaToken.Text = _currentUser.PixelaToken ?? "";
                
                // Last Backup
                await UpdateLastBackupLabel();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load settings");
            }
        }
        
        private async Task UpdateLastBackupLabel()
        {
            try
            {
                if (_currentUser == null) return;
                var backups = await _backupService.GetBackupsAsync(_currentUser);
                if (backups.Any())
                {
                    var latest = backups.OrderByDescending(b => b.Date).First();
                    lblLastBackup.Text = $"Last backup: {latest.Date:g}";
                }
                else
                {
                    lblLastBackup.Text = "Last backup: Never";
                }
            }
            catch
            {
                lblLastBackup.Text = "Last backup: Unknown";
            }
        }

        private async void BtnSaveSettings_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentUser == null)
                {
                    _currentUser = await _userService.GetCurrentUserAsync();
                }
                if (_currentUser == null) return;

                // KegomoDoro settings
                int workMin = (int)spinWorkMin.Value;
                int shortMin = (int)spinShortBreak.Value;
                int longMin = (int)spinLongBreak.Value;
                string hexColor = ColorTranslator.ToHtml(colorPickBack.Color);

                await _kegomoDoroService.UpdateConfigurationAsync(workMin, shortMin, longMin);
                await _kegomoDoroService.UpdateThemeAsync(hexColor);
                
                // API Keys
                var newApiKey = txtGeminiKey.Text.Trim();
                _currentUser.GeminiApiKey = newApiKey;
                _currentUser.PixelaUsername = txtPixelaUser.Text.Trim();
                _currentUser.PixelaToken = txtPixelaToken.Text.Trim();
                
                // Set API Key in Prometheus Service immediately
                if (!string.IsNullOrEmpty(newApiKey))
                {
                    _prometheusService.SetApiKey(newApiKey);
                    _aiProvider.Configure(newApiKey);
                    _logger.Information("Prometheus API key updated");
                }
                
                await _userService.UpdateUserAsync(_currentUser);

                XtraMessageBox.Show("All settings saved successfully! Prometheus is now connected.", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save settings");
                XtraMessageBox.Show("Failed to save settings: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void BtnCreateBackup_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentUser == null) return;
                
                var result = await _backupService.CreateBackupAsync(_currentUser);
                if (result.Success)
                {
                    await UpdateLastBackupLabel();
                    XtraMessageBox.Show("Backup created successfully!", "Success", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    XtraMessageBox.Show($"Failed to create backup: {result.Error}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to create backup");
                XtraMessageBox.Show("Backup failed: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnShowCredentials_Click(object? sender, EventArgs e)
        {
            // Toggle password char
            bool isMasked = txtGeminiKey.Properties.PasswordChar == '●';
            char newChar = isMasked ? '\0' : '●';
            
            txtGeminiKey.Properties.PasswordChar = newChar;
            txtPixelaToken.Properties.PasswordChar = newChar;
            
            btnShowCredentials.Text = isMasked ? "Hide" : "Show";
        }
        
        private void BtnOpenBackupFolder_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentUser == null) return;
                
                string path = _backupService.GetBackupPath(_currentUser);
                if (Directory.Exists(path))
                {
                    System.Diagnostics.Process.Start("explorer.exe", path);
                }
                else
                {
                    XtraMessageBox.Show($"Backup directory not found at: {path}", "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to open backup folder");
                XtraMessageBox.Show("Failed to open folder: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private async void BtnRestoreBackup_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_currentUser == null) return;
                
                var backups = await _backupService.GetBackupsAsync(_currentUser);
                if (!backups.Any())
                {
                    XtraMessageBox.Show("No backups available to restore.", "Info", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                
                // Show list of available backups
                var backupList = backups.OrderByDescending(b => b.Date).ToList();
                var items = backupList.Select(b => $"{b.Date:g} - {b.Type} ({b.SizeBytes / 1024} KB)").ToArray();
                
                // Simple selection using first available backup for now
                var confirm = XtraMessageBox.Show(
                    $"Restore the most recent backup from {backupList.First().Date:g}?\nThe application will restart.",
                    "Confirm Restore",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                    
                if (confirm == DialogResult.Yes)
                {
                    var success = await _backupService.RestoreBackupAsync(_currentUser, backupList.First().Date);
                    if (success)
                    {
                        XtraMessageBox.Show("Backup restored. The application will now restart.", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                    else
                    {
                        XtraMessageBox.Show("Restore failed.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to restore backup");
                XtraMessageBox.Show("Restore failed: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void BtnLogout_Click(object? sender, EventArgs e)
        {
            var confirm = XtraMessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
                
            if (confirm == DialogResult.Yes)
            {
                LogoutRequested?.Invoke(this, EventArgs.Empty);
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

            // Accent Line (Bottom) - Emerald for Settings
            using (var pen = new Pen(Color.FromArgb(100, 0, 200, 100), 2))
            {
                g.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
            }
        }
    }
}
