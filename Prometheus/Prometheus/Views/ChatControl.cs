using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using Svg;

namespace KeganOS.Views
{
    public partial class ChatControl : XtraUserControl
    {
        private readonly IPrometheusService _prometheusService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<ChatControl>();
        private readonly List<(string Role, string Message)> _conversationHistory = new();
        private readonly BindingList<ChatMessageViewModel> _chatMessages = new();
        
        private Image? _aiAvatarImage;
        private Image? _userAvatarImage;
        private const int AvatarSize = 36;
        private System.Windows.Forms.Timer _thinkingTimer;
        private int _thinkingDotCount = 0;

        public ChatControl(IPrometheusService prometheusService, IUserService userService)
        {
            _prometheusService = prometheusService;
            _userService = userService;
            InitializeComponent();
            
            _thinkingTimer = new System.Windows.Forms.Timer { Interval = 500 };
            _thinkingTimer.Tick += (s, e) => {
                _thinkingDotCount = (_thinkingDotCount + 1) % 4;
                if (_chatMessages.Count > 0 && _chatMessages.Last().IsThinking)
                {
                    lstChatHistory.Refresh();
                }
            };
            
            SetupChatUI();
            
            this.btnSend.Click += BtnSend_Click;
            this.txtUserInput.KeyDown += TxtUserInput_KeyDown;
            this.pnlHeader.Paint += PnlHeader_Paint;
        }

        private void SetupChatUI()
        {
            lstChatHistory.DataSource = _chatMessages;
            
            // Load AI Avatar from SVG
            try
            {
                var svgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Themes", "artificial-intelligence-5.svg");
                if (File.Exists(svgPath))
                {
                    var svgDoc = SvgDocument.Open(svgPath);
                    _aiAvatarImage = svgDoc.Draw(AvatarSize, AvatarSize);
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Failed to load AI avatar SVG");
            }
            
            // Auto-size items based on content
            lstChatHistory.MeasureItem += (s, e) => {
                var model = lstChatHistory.GetItem(e.Index) as ChatMessageViewModel;
                if (model != null) {
                    if (model.IsThinking)
                    {
                        e.ItemHeight = 70; // Fixed height for thinking
                        return;
                    }
                    var text = string.IsNullOrEmpty(model.Text) ? "..." : model.Text;
                    int listWidth = lstChatHistory.ClientSize.Width;
                    int bubbleWidth = (int)(listWidth * 0.75f) - 40; // Approx bubble width minus padding
                    
                    using var g = lstChatHistory.CreateGraphics();
                    using var font = new Font("Segoe UI", 10);
                    var size = g.MeasureString(text, font, bubbleWidth);
                    e.ItemHeight = Math.Max(70, (int)size.Height + 50); // Height + padding + timestamp
                }
            };

            lstChatHistory.DrawItem += (s, e) => {
                var model = lstChatHistory.GetItem(e.Index) as ChatMessageViewModel;
                if (model == null) return;

                e.Cache.FillRectangle(e.Appearance.BackColor, e.Bounds);
                bool isUser = model.Role == "user";
                
                var fullRect = e.Bounds;
                fullRect.Inflate(-10, -5);
                
                // Calculate avatar position
                int avatarPadding = AvatarSize + 10;
                Rectangle avatarRect;
                Rectangle bubbleRect;
                
                // Calculate available width logic
                int maxBubbleWidth = (int)(fullRect.Width * 0.75f);
                int bubbleWidth;
                
                if (model.IsThinking)
                {
                    bubbleWidth = 120; // Fixed width for thinking
                }
                else
                {
                    var textToShow = string.IsNullOrEmpty(model.Text) ? "..." : model.Text;
                    
                    // Measure exact required size
                    SizeF textSize;
                    using (var measureFont = new Font("Segoe UI", 10))
                    {
                        textSize = e.Graphics.MeasureString(textToShow, measureFont, maxBubbleWidth);
                    }
                    
                    bubbleWidth = (int)textSize.Width + 35; // Text width + internal padding
                    bubbleWidth = Math.Max(bubbleWidth, 60); // Min width
                    bubbleWidth = Math.Min(bubbleWidth, maxBubbleWidth); // Max width
                }

                // Avatar Position
                if (isUser)
                {
                    // Avatar on right
                    avatarRect = new Rectangle(fullRect.Right - AvatarSize, fullRect.Y + 5, AvatarSize, AvatarSize);
                    // Bubble to left of avatar
                    bubbleRect = new Rectangle(avatarRect.Left - 10 - bubbleWidth, fullRect.Y, bubbleWidth, fullRect.Height);
                }
                else
                {
                    // Avatar on left
                    avatarRect = new Rectangle(fullRect.X, fullRect.Y + 5, AvatarSize, AvatarSize);
                    // Bubble to right of avatar
                    bubbleRect = new Rectangle(avatarRect.Right + 10, fullRect.Y, bubbleWidth, fullRect.Height);
                }

                // Draw Avatar
                Image? avatarToDraw = isUser ? _userAvatarImage : _aiAvatarImage;
                
                // Enable high-quality rendering
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                
                if (avatarToDraw != null)
                {
                    // Draw circular avatar with high quality
                    using var path = new System.Drawing.Drawing2D.GraphicsPath();
                    path.AddEllipse(avatarRect);
                    e.Graphics.SetClip(path);
                    e.Graphics.DrawImage(avatarToDraw, avatarRect);
                    e.Graphics.ResetClip();
                    
                    // Draw smooth border
                    using var pen = new Pen(isUser ? Color.FromArgb(0, 120, 212) : Color.Cyan, 2);
                    e.Graphics.DrawEllipse(pen, avatarRect);
                }
                else
                {
                    // Fallback: Draw initials circle
                    using var bgBrush = new SolidBrush(isUser ? Color.FromArgb(0, 120, 212) : Color.FromArgb(80, 80, 80));
                    e.Graphics.FillEllipse(bgBrush, avatarRect);
                    using var font = new Font("Segoe UI", 12, FontStyle.Bold);
                    string initials = isUser ? "U" : "P";
                    var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    e.Graphics.DrawString(initials, font, Brushes.White, avatarRect, sf);
                }

                // Draw bubble with glassy effect
                Color bubbleColor = isUser ? Color.FromArgb(0, 120, 212) : Color.FromArgb(40, 45, 50);
                
                // Glass highlight for AI bubbles
                using (var bubblePath = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int radius = 14;
                    bubblePath.AddArc(bubbleRect.X, bubbleRect.Y, radius * 2, radius * 2, 180, 90);
                    bubblePath.AddArc(bubbleRect.Right - radius * 2, bubbleRect.Y, radius * 2, radius * 2, 270, 90);
                    bubblePath.AddArc(bubbleRect.Right - radius * 2, bubbleRect.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                    bubblePath.AddArc(bubbleRect.X, bubbleRect.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                    bubblePath.CloseFigure();
                    
                    using (var bubbleBrush = new SolidBrush(bubbleColor))
                    {
                        e.Graphics.FillPath(bubbleBrush, bubblePath);
                    }
                    
                    // Subtle border for glass effect
                    using (var borderPen = new Pen(isUser ? Color.FromArgb(60, 255, 255, 255) : Color.FromArgb(40, 0, 255, 255), 1))
                    {
                        e.Graphics.DrawPath(borderPen, bubblePath);
                    }
                }
                
                // Draw Text
                var textRect = bubbleRect;
                textRect.Inflate(-14, -10);
                using (var font2 = new Font("Segoe UI", 10))
                {
                     if (model.IsThinking)
                     {
                         string dots = new string('.', _thinkingDotCount);
                         e.Graphics.DrawString($"Thinking{dots}", font2, Brushes.Gray, textRect, StringFormat.GenericDefault);
                     }
                     else
                     {
                        e.Cache.DrawString(model.Text, font2, Brushes.White, textRect, StringFormat.GenericDefault);
                     }
                }
                
                // Draw timestamp
                using (var timeFont = new Font("Segoe UI", 7))
                {
                    var timeText = model.Time;
                    var timeSize = e.Graphics.MeasureString(timeText, timeFont);
                    var timeX = isUser ? bubbleRect.Right - timeSize.Width - 10 : bubbleRect.X + 10;
                    var timeY = bubbleRect.Bottom - timeSize.Height - 4;
                    e.Graphics.DrawString(timeText, timeFont, Brushes.Gray, timeX, timeY);
                }
                
                e.Handled = true;
            };
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            // Load user avatar
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                if (user != null && user.HasAvatar)
                {
                    using var originalImg = Image.FromFile(user.AvatarPath);
                    _userAvatarImage = new Bitmap(originalImg, new Size(AvatarSize, AvatarSize));
                }
            }
            catch (Exception ex)
            {
                _logger.Warning(ex, "Failed to load user avatar");
            }

            AppendMessage("Prometheus", "Hello! I am your companion, Prometheus. How can I assist you in your journey today? :)");
            txtUserInput.Focus();
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await SendMessageAsync();
        }

        private async void TxtUserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await SendMessageAsync();
            }
        }

        private async Task SendMessageAsync()
        {
            string userText = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userText)) return;

            txtUserInput.Text = "";
            txtUserInput.Enabled = false;
            btnSend.Enabled = false;
            lblTypingIndicator.Visible = false; // We use bubble animation now

            AppendMessage("User", userText);
            _conversationHistory.Add(("user", userText));

            // Prepare AI response area with Thinking state
            var responseVm = new ChatMessageViewModel
            {
                Sender = "Prometheus",
                Text = "",
                Time = DateTime.Now.ToString("HH:mm"),
                Role = "assistant",
                IsThinking = true
            };
            _chatMessages.Add(responseVm);
            ScrollToBottom();
            
            _thinkingTimer.Start();
            
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                var fullResponse = new StringBuilder();

                bool firstChunk = true;
                await foreach (var chunk in _prometheusService.ConsultStreamingAsync(userText, user?.Id, _conversationHistory))
                {
                    if (firstChunk)
                    {
                        // Stop thinking animation on first chunk
                        responseVm.IsThinking = false;
                        _thinkingTimer.Stop();
                        firstChunk = false;
                    }
                    
                    fullResponse.Append(chunk);
                    responseVm.Text = fullResponse.ToString();
                    
                    // Force re-measure and repaint
                    // Toggling an item in BindingList usually triggers list updates, 
                    // but for smooth streaming we need to manually trigger LayoutChanged if possible.
                    // For standard ListBox, we rely on ResetItem or Refresh.
                    _chatMessages.ResetItem(_chatMessages.Count - 1);
                    lstChatHistory.Refresh(); 
                    ScrollToBottom(); // Keep scrolling
                }

                _conversationHistory.Add(("assistant", fullResponse.ToString()));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Chat failed");
                _thinkingTimer.Stop();
                responseVm.IsThinking = false;
                responseVm.Text = "I'm sorry, I'm having trouble connecting to my core right now. :( Error: " + ex.Message;
                _chatMessages.ResetItem(_chatMessages.Count - 1);
            }
            finally
            {
                txtUserInput.Enabled = true;
                btnSend.Enabled = true;
                txtUserInput.Focus();
            }
        }

        private void AppendMessage(string sender, string message)
        {
            string role = sender.Equals("User", StringComparison.OrdinalIgnoreCase) ? "user" : "assistant";
            var vm = new ChatMessageViewModel
            {
                Sender = sender,
                Text = message,
                Time = DateTime.Now.ToString("HH:mm"),
                Role = role
            };
            _chatMessages.Add(vm);
            ScrollToBottom();
        }

        private void UpdateLastMessage(string fullText)
        {
            if (_chatMessages.Count > 0)
            {
                var last = _chatMessages[_chatMessages.Count - 1];
                last.Text = fullText;
                lstChatHistory.Refresh();
            }
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            lstChatHistory.TopIndex = lstChatHistory.ItemCount - 1;
        }
        
        private void PnlHeader_Paint(object? sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = pnlHeader.ClientRectangle;

            // Glass Background (Simulated)
            using (var brush = new System.Drawing.Drawing2D.LinearGradientBrush(rect, 
                Color.FromArgb(60, 255, 255, 255), 
                Color.FromArgb(10, 255, 255, 255), 45f))
            {
                g.FillRectangle(brush, rect);
            }

            // Accent Line (Bottom) - Cyan for Chat
            using (var pen = new Pen(Color.FromArgb(100, 0, 255, 255), 2))
            {
                g.DrawLine(pen, 0, rect.Height - 1, rect.Width, rect.Height - 1);
            }
        }
    }


    public class ChatMessageViewModel
    {
        public string Sender { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
        public string Role { get; set; }
        public bool IsThinking { get; set; }
    }
}
