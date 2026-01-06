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
    public partial class AchievementsControl : XtraUserControl
    {
        private readonly IAchievementService _achievementService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<AchievementsControl>();

        public AchievementsControl(IAchievementService achievementService, IUserService userService)
        {
            _achievementService = achievementService;
            _userService = userService;
            InitializeComponent();

            // Glassmorphism Header Paint
            this.Paint += (s, pe) => {
                var g = pe.Graphics;
                var rect = new Rectangle(0, 0, this.Width, 65);
                
                // Light glass background
                using var brush = new SolidBrush(Color.FromArgb(20, 0, 0, 0));
                g.FillRectangle(brush, rect);
                
                // Accent border
                using var pen = new Pen(Color.FromArgb(40, 0, 0, 0), 1);
                g.DrawLine(pen, 0, 64, this.Width, 64);
            };
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            await LoadAchievementsAsync();
        }

        private async Task LoadAchievementsAsync()
        {
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user == null) return;

                var achievements = _achievementService.GetAchievements(user);
                
                tileGroupMain.Items.Clear();

                foreach (var achievement in achievements)
                {
                    TileItem item = new TileItem();
                    item.ItemSize = TileItemSize.Medium;
                    
                    // Main Text (Name)
                    TileItemElement elementTitle = new TileItemElement();
                    elementTitle.Text = achievement.Name;
                    elementTitle.TextAlignment = TileItemContentAlignment.TopLeft;
                    elementTitle.Appearance.Normal.Font = new Font("Segoe UI Semibold", 11);
                    item.Elements.Add(elementTitle);

                    // Icon/Emoji
                    TileItemElement elementIcon = new TileItemElement();
                    elementIcon.Text = achievement.IsUnlocked ? achievement.Icon : "🔒";
                    elementIcon.TextAlignment = TileItemContentAlignment.MiddleCenter;
                    elementIcon.Appearance.Normal.Font = new Font("Segoe UI", 32);
                    item.Elements.Add(elementIcon);

                    // Subtext (Description)
                    TileItemElement elementDesc = new TileItemElement();
                    elementDesc.Text = achievement.Description;
                    elementDesc.TextAlignment = TileItemContentAlignment.BottomLeft;
                    elementDesc.Appearance.Normal.Font = new Font("Segoe UI", 8.5F);
                    item.Elements.Add(elementDesc);

                    item.AppearanceItem.Normal.BorderColor = Color.FromArgb(40, 0, 0, 0);
                    item.AppearanceItem.Normal.Options.UseBorderColor = true;

                    if (achievement.IsUnlocked)
                    {
                        item.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(achievement.Color);
                        item.AppearanceItem.Normal.ForeColor = Color.White;
                        item.AppearanceItem.Normal.Options.UseBackColor = true;
                        elementDesc.Appearance.Normal.ForeColor = Color.FromArgb(220, 255, 255, 255);
                    }
                    else
                    {
                        item.AppearanceItem.Normal.BackColor = Color.White;
                        item.AppearanceItem.Normal.ForeColor = Color.FromArgb(120, 120, 120);
                        item.AppearanceItem.Normal.Options.UseBackColor = true;
                        elementIcon.Appearance.Normal.ForeColor = Color.FromArgb(100, 120, 120, 120);
                        elementDesc.Appearance.Normal.ForeColor = Color.FromArgb(150, 120, 120, 120);
                    }

                    item.Tag = achievement;
                    tileGroupMain.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load achievements");
            }
        }
    }
}
