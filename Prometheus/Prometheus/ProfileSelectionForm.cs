using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using Microsoft.Extensions.DependencyInjection;

namespace KeganOS
{
    public partial class ProfileSelectionForm : XtraForm
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger = Log.ForContext<ProfileSelectionForm>();
        
        public ProfileSelectionForm(IUserService userService, IServiceProvider serviceProvider)
        {
            _userService = userService;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            tileControl1.Cursor = Cursors.Hand;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                DisplayUsers(users);
                
                // If NO users exist, automatically trigger profile creation
                if (!users.Any())
                {
                    _logger.Information("No users found, triggering profile creation");
                    CreateNewProfile();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to load profiles");
                XtraMessageBox.Show("Failed to load user profiles.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayUsers(IEnumerable<User> users)
        {
            tileControl1.Groups[0].Items.Clear();
            
            foreach (var user in users)
            {
                var item = new TileItem();
                item.Tag = user;
                item.ItemSize = TileItemSize.Medium;
                
                // Element 0: Circular Avatar
                var elImage = new TileItemElement();
                elImage.ImageAlignment = TileItemContentAlignment.MiddleCenter;
                elImage.ImageScaleMode = TileItemImageScaleMode.ZoomInside;
                
                if (user.HasAvatar && System.IO.File.Exists(user.AvatarPath))
                {
                    try
                    {
                        using (var original = Image.FromFile(user.AvatarPath))
                        {
                            elImage.Image = GetCircularImage(original, 80);
                        }
                    }
                    catch { /* Fallback to default or nothing */ }
                }
                
                // Element 1: Display Name
                var elText = new TileItemElement();
                elText.Text = user.DisplayName;
                elText.TextAlignment = TileItemContentAlignment.BottomCenter;
                elText.Appearance.Normal.Font = new Font("Segoe UI Semibold", 10F);
                
                item.Elements.Add(elImage);
                item.Elements.Add(elText);
                
                tileControl1.Groups[0].Items.Add(item);
            }
            
            // Add "Create New" item
            var newItem = new TileItem();
            var elNewText = new TileItemElement();
            elNewText.Text = "+ New Profile";
            elNewText.TextAlignment = TileItemContentAlignment.MiddleCenter;
            newItem.Elements.Add(elNewText);
            
            newItem.ItemSize = TileItemSize.Medium;
            newItem.Tag = "NEW";
            tileControl1.Groups[0].Items.Add(newItem);
        }

        private Image GetCircularImage(Image img, int size)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, size, size);
                    g.SetClip(path);
                    g.DrawImage(img, new Rectangle(0, 0, size, size));
                }
            }
            return bitmap;
        }

        private async void tileControl1_ItemClick(object sender, TileItemEventArgs e)
        {
            if (e.Item.Tag is User user)
            {
                _logger.Information("Selected user: {User}", user.DisplayName);
                await _userService.SetLastActiveUserIdAsync(user.Id);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else if (e.Item.Tag?.ToString() == "NEW")
            {
                CreateNewProfile();
            }
        }

        private async void CreateNewProfile()
        {
            using (var scope = Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.CreateScope(_serviceProvider))
            {
                var createForm = scope.ServiceProvider.GetRequiredService<CreateProfileForm>();
                if (createForm.ShowDialog() == DialogResult.OK && createForm.CreatedUser != null)
                {
                    await _userService.CreateUserAsync(createForm.CreatedUser);
                    var users = await _userService.GetAllUsersAsync();
                    DisplayUsers(users);
                    
                    // If this was the first user, select them automatically
                    if (users.Count() == 1)
                    {
                        var firstUser = users.First();
                        await _userService.SetLastActiveUserIdAsync(firstUser.Id);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }
    }
}
