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
                    elementTitle.TextAlignment = TileItemContentAlignment.BottomLeft;
                    elementTitle.Appearance.Normal.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    item.Elements.Add(elementTitle);

                    // Icon/Emoji
                    TileItemElement elementIcon = new TileItemElement();
                    elementIcon.Text = achievement.Icon;
                    elementIcon.TextAlignment = TileItemContentAlignment.TopRight;
                    elementIcon.Appearance.Normal.Font = new Font("Segoe UI", 24);
                    item.Elements.Add(elementIcon);

                    // Subtext (Description)
                    TileItemElement elementDesc = new TileItemElement();
                    elementDesc.Text = achievement.Description;
                    elementDesc.TextAlignment = TileItemContentAlignment.BottomLeft;
                    elementDesc.Appearance.Normal.Font = new Font("Segoe UI", 8);
                    elementDesc.Appearance.Normal.ForeColor = Color.FromArgb(180, 180, 180);
                    // Add some margin/offset for description
                    item.Elements.Add(elementDesc);

                    if (achievement.IsUnlocked)
                    {
                        item.AppearanceItem.Normal.BackColor = ColorTranslator.FromHtml(achievement.Color);
                        item.AppearanceItem.Normal.ForeColor = Color.White;
                    }
                    else
                    {
                        item.AppearanceItem.Normal.BackColor = Color.FromArgb(40, 40, 40);
                        item.AppearanceItem.Normal.ForeColor = Color.FromArgb(100, 100, 100);
                        elementIcon.Appearance.Normal.ForeColor = Color.FromArgb(100, 100, 100);
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
