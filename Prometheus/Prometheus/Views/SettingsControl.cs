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
    public partial class SettingsControl : XtraUserControl
    {
        private readonly IUserService _userService;
        private readonly IKegomoDoroService _kegomoDoroService;
        private readonly ILogger _logger = Log.ForContext<SettingsControl>();
        private UserPreferences? _preferences;

        public SettingsControl(IUserService userService, IKegomoDoroService kegomoDoroService)
        {
            _userService = userService;
            _kegomoDoroService = kegomoDoroService;
            InitializeComponent();

            this.btnSaveSettings.Click += BtnSaveSettings_Click;
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
                var user = await _userService.GetCurrentUserAsync();
                _preferences = await _kegomoDoroService.GetConfigurationAsync(user);

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
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load settings");
            }
        }

        private async void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                int workMin = (int)spinWorkMin.Value;
                int shortMin = (int)spinShortBreak.Value;
                int longMin = (int)spinLongBreak.Value;
                string hexColor = ColorTranslator.ToHtml(colorPickBack.Color);

                // Update KegomoDoro Service (writes files)
                await _kegomoDoroService.UpdateConfigurationAsync(workMin, shortMin, longMin);
                await _kegomoDoroService.UpdateThemeAsync(hexColor);

                XtraMessageBox.Show("Settings saved and synced to KegomoDoro successfully!", "Success", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save settings");
                XtraMessageBox.Show("Failed to save settings: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
