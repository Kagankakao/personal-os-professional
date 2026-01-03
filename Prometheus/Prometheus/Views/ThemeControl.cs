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
using DevExpress.XtraBars.Ribbon;

namespace KeganOS.Views
{
    public partial class ThemeControl : XtraUserControl
    {
        private readonly IThemeService _themeService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<ThemeControl>();

        public ThemeControl(IThemeService themeService, IUserService userService)
        {
            _themeService = themeService;
            _userService = userService;
            InitializeComponent();

            this.galleryControlThemes.Gallery.ItemClick += Gallery_ItemClick;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await LoadThemesAsync();
        }

        private async Task LoadThemesAsync()
        {
            try
            {
                var themes = await _themeService.GetAvailableThemesAsync();
                var currentTheme = await _themeService.GetCurrentThemeAsync();

                DevExpress.XtraBars.Ribbon.GalleryItemGroup group = new DevExpress.XtraBars.Ribbon.GalleryItemGroup { Caption = "App Themes" };
                
                foreach (var theme in themes)
                {
                    DevExpress.XtraBars.Ribbon.GalleryItem item = new DevExpress.XtraBars.Ribbon.GalleryItem { Caption = theme.Name, Description = theme.Description, Tag = theme };
                    
                    // Try to load a thumbnail if available, otherwise use a placeholder color
                    item.ImageOptions.Image = CreateThemeThumbnail(theme);
                    
                    if (theme.Id == currentTheme?.Id) item.Checked = true;
                    group.Items.Add(item);
                }

                galleryControlThemes.Gallery.Groups.Clear();
                galleryControlThemes.Gallery.Groups.Add(group);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load themes");
            }
        }

        private Image CreateThemeThumbnail(Theme theme)
        {
            // Simple placeholder: a solid block of the theme's background color
            Bitmap bmp = new Bitmap(120, 80);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                try
                {
                    Color back = ColorTranslator.FromHtml(theme.BackgroundColor);
                    Color accent = ColorTranslator.FromHtml(theme.AccentColor);
                    
                    g.Clear(back);
                    using (Pen p = new Pen(accent, 4))
                    {
                        g.DrawRectangle(p, 10, 10, 100, 60);
                    }
                }
                catch
                {
                    g.Clear(Color.Gray);
                }
            }
            return bmp;
        }

        private async void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
        {
            if (e.Item.Tag is Theme theme)
            {
                try
                {
                    var user = await _userService.GetCurrentUserAsync();
                    bool success = await _themeService.ApplyThemeAsync(theme, user);
                    
                    if (success)
                    {
                        XtraMessageBox.Show($"Theme '{theme.Name}' applied successfully! Restart may be required for some components.", 
                            "Theme Palace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Failed to apply theme");
                    XtraMessageBox.Show("Failed to apply theme: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
