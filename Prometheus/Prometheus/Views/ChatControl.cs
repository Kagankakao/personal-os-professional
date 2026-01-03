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
    public partial class ChatControl : XtraUserControl
    {
        private readonly IPrometheusService _prometheusService;
        private readonly IUserService _userService;
        private readonly ILogger _logger = Log.ForContext<ChatControl>();
        private readonly List<(string Role, string Message)> _conversationHistory = new();

        public ChatControl(IPrometheusService prometheusService, IUserService userService)
        {
            _prometheusService = prometheusService;
            _userService = userService;
            InitializeComponent();
            
            this.btnSend.Click += BtnSend_Click;
            this.txtUserInput.KeyDown += TxtUserInput_KeyDown;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

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

            AppendMessage("User", userText);
            _conversationHistory.Add(("user", userText));

            // Prepare AI response area
            AppendMessage("Prometheus", "");
            
            try
            {
                var user = await _userService.GetCurrentUserAsync();
                var fullResponse = new StringBuilder();

                await foreach (var chunk in _prometheusService.ConsultStreamingAsync(userText, user?.Id, _conversationHistory))
                {
                    fullResponse.Append(chunk);
                    UpdateLastMessage(fullResponse.ToString());
                }

                _conversationHistory.Add(("assistant", fullResponse.ToString()));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Chat failed");
                UpdateLastMessage("I'm sorry, I'm having trouble connecting to my core right now. :( Error: " + ex.Message);
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
            string timestamp = DateTime.Now.ToString("HH:mm");
            string formatted = $"[{timestamp}] {sender}: {message}\r\n\r\n";
            txtChatHistory.Text += formatted;
            ScrollToBottom();
        }

        private void UpdateLastMessage(string fullText)
        {
            // This is a simple implementation for MemoEdit. 
            // Better to use a richer control or custom drawing for a real chat UI.
            var lines = txtChatHistory.Lines.ToList();
            if (lines.Count >= 2)
            {
                // Find the last message (marked by Prometheus:)
                int lastIdx = txtChatHistory.Text.LastIndexOf("Prometheus: ");
                if (lastIdx >= 0)
                {
                    string header = txtChatHistory.Text.Substring(0, lastIdx + 12);
                    txtChatHistory.Text = header + fullText + "\r\n\r\n";
                }
            }
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            txtChatHistory.SelectionStart = txtChatHistory.Text.Length;
            txtChatHistory.ScrollToCaret();
        }
    }
}
