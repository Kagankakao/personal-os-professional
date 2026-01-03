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

namespace KeganOS
{
    public partial class ProfileSelectionForm : XtraForm
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<ProfileSelectionForm>();
        
        public ProfileSelectionForm(IUserService userService)
        {
            _userService = userService;
            InitializeComponent();
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
            
            try
            {
                var users = await _userService.GetAllUsersAsync();
                DisplayUsers(users);
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
                item.Text = user.DisplayName;
                item.Tag = user;
                item.Appearance.BackColor = Color.FromArgb(40, 40, 40);
                item.Appearance.ForeColor = Color.White;
                item.ItemSize = TileItemSize.Medium;
                
                // Image handling would go here
                
                tileControl1.Groups[0].Items.Add(item);
            }
            
            // Add "Create New" item
            var newItem = new TileItem();
            newItem.Text = "+ New Profile";
            newItem.Appearance.BackColor = Color.DarkRed;
            newItem.ItemSize = TileItemSize.Medium;
            newItem.Tag = "NEW";
            tileControl1.Groups[0].Items.Add(newItem);
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
            // Simple prompt for now
            string name = XtraInputBox.Show("Enter Profile Name:", "New Profile", "");
            if (!string.IsNullOrEmpty(name))
            {
                var user = new User { DisplayName = name, JournalFileName = $"{name}_Journal.txt" };
                await _userService.CreateUserAsync(user);
                var users = await _userService.GetAllUsersAsync();
                DisplayUsers(users);
            }
        }
    }
}
