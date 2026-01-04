using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using System.Net.Http;
using System.Drawing;

namespace KeganOS
{
    public partial class CreateProfileForm : XtraForm
    {
        private readonly ILogger _logger = Log.ForContext<CreateProfileForm>();
        private readonly IPixelaService _pixelaService;
        private string? _selectedAvatarPath;
        private string? _selectedJournalPath;
        private bool _isNewPixelaUser = false;
        private bool _pixelaChecked = false;

        public User? CreatedUser { get; private set; }

        public CreateProfileForm(IPixelaService pixelaService)
        {
            _pixelaService = pixelaService;
            InitializeComponent();
            
            // Wire up events
            this.btnBrowseAvatar.Click += BtnBrowseAvatar_Click;
            this.btnBrowseJournal.Click += BtnBrowseJournal_Click;
            this.btnCheckPixela.Click += BtnCheckPixela_Click;
            this.btnCreate.Click += BtnCreate_Click;
            this.btnCancel.Click += (s, e) => Close();
            
            this.txtDisplayName.EditValueChanged += (s, e) => UpdateJournalHint();
        }

        private void UpdateJournalHint()
        {
            if (string.IsNullOrEmpty(_selectedJournalPath))
            {
                string name = txtDisplayName.Text.Trim();
                if (!string.IsNullOrEmpty(name))
                {
                    lblJournalPath.Text = $"{name.Replace(" ", "_").ToLower()}.txt (Auto-gen)";
                }
            }
        }

        private void BtnBrowseAvatar_Click(object? sender, EventArgs e)
        {
            using var openFile = new OpenFileDialog
            {
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.gif;*.bmp",
                Title = "Select Avatar"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                _selectedAvatarPath = openFile.FileName;
                picAvatar.Image = System.Drawing.Image.FromFile(_selectedAvatarPath);
            }
        }

        private void BtnBrowseJournal_Click(object? sender, EventArgs e)
        {
            using var openFile = new OpenFileDialog
            {
                Filter = "Journal Files (*.txt)|*.txt|All Files|*.*",
                Title = "Select KEGOMODORO Journal"
            };

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                _selectedJournalPath = openFile.FileName;
                ValidateJournalFile(_selectedJournalPath);
            }
        }

        private void ValidateJournalFile(string filePath)
        {
            try
            {
                var content = File.ReadAllText(filePath);
                var lines = content.Split('\n');
                
                // Ported regex from KeganOS
                var datePattern = new System.Text.RegularExpressions.Regex(@"^\d{1,2}[/.]?\d{1,2}[/.]?\d{4}");
                var timePattern = new System.Text.RegularExpressions.Regex(@"^\d{1,2}:\d{2}");
                
                var uniqueDates = new System.Collections.Generic.HashSet<string>();
                int timeEntries = 0;
                int noteLines = 0;
                
                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    if (string.IsNullOrEmpty(trimmed)) continue;
                    
                    if (datePattern.IsMatch(trimmed))
                        uniqueDates.Add(trimmed);
                    else if (timePattern.IsMatch(trimmed))
                        timeEntries++;
                    else
                        noteLines++;
                }
                
                int dateEntries = uniqueDates.Count;
                if (dateEntries > 0)
                {
                    lblJournalPath.Appearance.ForeColor = System.Drawing.Color.FromArgb(136, 204, 136); // Greenish
                    lblJournalPath.Text = $"✓ {dateEntries} days tracked, {timeEntries} logs";
                    if (noteLines > 0) lblJournalPath.Text += $", {noteLines} notes";
                }
                else
                {
                    lblJournalPath.Appearance.ForeColor = System.Drawing.Color.FromArgb(204, 204, 136); // Yellowish
                    lblJournalPath.Text = noteLines > 0 
                        ? $"📝 {noteLines} lines found (no dates)" 
                        : "📄 Empty file - ready to start!";
                }
                
                lblJournalPath.ToolTip = filePath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to validate journal file");
                lblJournalPath.Appearance.ForeColor = System.Drawing.Color.FromArgb(204, 136, 136); // Reddish
                lblJournalPath.Text = "✗ Error reading file";
            }
        }

        private async void BtnCheckPixela_Click(object? sender, EventArgs e)
        {
            string username = txtPixelaUser.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(username))
            {
                XtraMessageBox.Show("Please enter a Pixela username.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnCheckPixela.Enabled = false;
            btnCheckPixela.Text = "...";

            try
            {
                // Simple format check (ported from KeganOS)
                var (isValid, error) = await _pixelaService.CheckUsernameAvailabilityAsync(username);
                if (!isValid)
                {
                    XtraMessageBox.Show($"Invalid format: {error}", "Pixela Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if user exists on pixe.la
                using var client = new System.Net.Http.HttpClient();
                client.Timeout = TimeSpan.FromSeconds(5);
                var response = await client.GetAsync($"https://pixe.la/@{username}");
                
                if (response.IsSuccessStatusCode)
                {
                    _isNewPixelaUser = false;
                    XtraMessageBox.Show("Username exists. Please provide your token.", "Pixela Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _isNewPixelaUser = true;
                    XtraMessageBox.Show("Username is available! We will register it for you.", "Pixela Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
                _pixelaChecked = true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to check Pixela username");
                XtraMessageBox.Show("Failed to connect to Pixe.la. Please check your internet.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCheckPixela.Enabled = true;
                btnCheckPixela.Text = "Check";
            }
        }

        private async void BtnCreate_Click(object? sender, EventArgs e)
        {
            string displayName = txtDisplayName.Text.Trim();
            if (string.IsNullOrEmpty(displayName))
            {
                XtraMessageBox.Show("Display Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string pixelaUser = txtPixelaUser.Text.Trim();
            if (!string.IsNullOrEmpty(pixelaUser) && !_pixelaChecked)
            {
                var result = XtraMessageBox.Show("You haven't checked the Pixela username. Continue anyway?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            var user = new User
            {
                DisplayName = displayName,
                PersonalSymbol = txtSymbol.Text.Trim(),
                GeminiApiKey = txtGeminiKey.Text.Trim(),
                CreatedAt = DateTime.Now,
                LastLoginAt = DateTime.Now
            };

            // Avatar
            if (!string.IsNullOrEmpty(_selectedAvatarPath))
            {
                user.AvatarPath = SaveAvatarLocally(_selectedAvatarPath, displayName);
            }

            // Journal
            user.JournalFileName = !string.IsNullOrEmpty(_selectedJournalPath) 
                ? Path.GetFileName(_selectedJournalPath) 
                : $"{displayName.Replace(" ", "_").ToLower()}.txt";

            // Pixela
            if (!string.IsNullOrEmpty(pixelaUser))
            {
                user.PixelaUsername = pixelaUser;
                user.PixelaGraphId = string.IsNullOrEmpty(txtPixelaGraph.Text) ? "focus" : txtPixelaGraph.Text.Trim();
                
                string token = txtPixelaToken.Text.Trim();
                if (string.IsNullOrEmpty(token) && _isNewPixelaUser)
                {
                    token = _pixelaService.GenerateToken(pixelaUser);
                }
                user.PixelaToken = token;

                if (_isNewPixelaUser && !string.IsNullOrEmpty(token))
                {
                    XtraMessageBox.Show("Attempting to register Pixela account... (Internet required)", "Pixela Setup");
                    var (success, error) = await _pixelaService.RegisterUserAsync(pixelaUser, token);
                    if (!success)
                    {
                        _logger.Warning("Failed to register Pixela user: {Error}", error);
                    }
                }
            }

            CreatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        private string SaveAvatarLocally(string sourcePath, string userName)
        {
            try
            {
                var avatarsDir = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "KeganOS", "avatars");
                
                Directory.CreateDirectory(avatarsDir);
                
                var extension = Path.GetExtension(sourcePath);
                var safeUserName = string.Join("_", userName.Split(Path.GetInvalidFileNameChars()));
                var destPath = Path.Combine(avatarsDir, $"{safeUserName}_{DateTime.Now:yyyyMMddHHmmss}{extension}");
                
                File.Copy(sourcePath, destPath, true);
                return destPath;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to save avatar locally");
                return sourcePath; // Fallback to original path if copy fails
            }
        }
    }
}
