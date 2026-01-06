namespace KeganOS.Views;

partial class ChatControl
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        this.lstChatHistory = new DevExpress.XtraEditors.ListBoxControl();
        this.panelInput = new DevExpress.XtraEditors.PanelControl();
        this.lblTypingIndicator = new DevExpress.XtraEditors.LabelControl();
        this.btnSend = new DevExpress.XtraEditors.SimpleButton();
        this.txtUserInput = new DevExpress.XtraEditors.MemoEdit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
        this.pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.lstChatHistory)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelInput)).BeginInit();
        this.panelInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtUserInput.Properties)).BeginInit();
        this.SuspendLayout();
        
        // pnlHeader
        this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlHeader.Controls.Add(this.lblTitle);
        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlHeader.Location = new System.Drawing.Point(20, 20);
        this.pnlHeader.Name = "pnlHeader";
        this.pnlHeader.Size = new System.Drawing.Size(560, 50);
        this.pnlHeader.TabIndex = 10;
        
        // lblTitle
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 22F);
        this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Appearance.Options.UseForeColor = true;
        this.lblTitle.Location = new System.Drawing.Point(0, 5);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(180, 40);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Prometheus AI";
        
        // lstChatHistory
        this.lstChatHistory.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lstChatHistory.Location = new System.Drawing.Point(20, 70);
        this.lstChatHistory.Name = "lstChatHistory";
        this.lstChatHistory.Size = new System.Drawing.Size(560, 280);
        this.lstChatHistory.TabIndex = 0;
        this.lstChatHistory.ItemHeight = 90;
        this.lstChatHistory.HorizontalScrollbar = false;
        this.lstChatHistory.SelectionMode = System.Windows.Forms.SelectionMode.None;
        this.lstChatHistory.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        // panelInput
        this.panelInput.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.panelInput.Controls.Add(this.btnSend);
        this.panelInput.Controls.Add(this.txtUserInput);
        this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelInput.Location = new System.Drawing.Point(20, 370);
        this.panelInput.Name = "panelInput";
        this.panelInput.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
        this.panelInput.Size = new System.Drawing.Size(560, 60);
        this.panelInput.TabIndex = 1;
        
        // lblTypingIndicator
        this.lblTypingIndicator.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
        this.lblTypingIndicator.Appearance.ForeColor = System.Drawing.Color.Cyan;
        this.lblTypingIndicator.Appearance.Options.UseFont = true;
        this.lblTypingIndicator.Appearance.Options.UseForeColor = true;
        this.lblTypingIndicator.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.lblTypingIndicator.Location = new System.Drawing.Point(20, 355);
        this.lblTypingIndicator.Name = "lblTypingIndicator";
        this.lblTypingIndicator.Padding = new System.Windows.Forms.Padding(10, 0, 0, 5);
        this.lblTypingIndicator.Size = new System.Drawing.Size(120, 15);
        this.lblTypingIndicator.TabIndex = 2;
        this.lblTypingIndicator.Text = "Prometheus is thinking...";
        this.lblTypingIndicator.Visible = false;
        
        // btnSend
        this.btnSend.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnSend.Appearance.ForeColor = System.Drawing.Color.White;
        this.btnSend.Appearance.Options.UseFont = true;
        this.btnSend.Appearance.Options.UseForeColor = true;
        this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnSend.Location = new System.Drawing.Point(470, 10);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new System.Drawing.Size(90, 50);
        this.btnSend.TabIndex = 1;
        this.btnSend.Text = "Consult";
        // txtUserInput
        this.txtUserInput.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtUserInput.Location = new System.Drawing.Point(0, 10);
        this.txtUserInput.Name = "txtUserInput";
        this.txtUserInput.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.txtUserInput.Properties.Appearance.Options.UseFont = true;
        this.txtUserInput.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        this.txtUserInput.Properties.NullValuePrompt = "Ask Prometheus anything...";
        this.txtUserInput.Properties.NullValuePromptShowForEmptyValue = true;
        this.txtUserInput.Size = new System.Drawing.Size(370, 50);
        this.txtUserInput.TabIndex = 0;
        
        // ChatControl
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.lstChatHistory);
        this.Controls.Add(this.pnlHeader);
        this.Controls.Add(this.lblTypingIndicator);
        this.Controls.Add(this.panelInput);
        this.Name = "ChatControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
        this.pnlHeader.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.lstChatHistory)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelInput)).EndInit();
        this.panelInput.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.txtUserInput.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraEditors.PanelControl pnlHeader;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraEditors.ListBoxControl lstChatHistory;
    private DevExpress.XtraEditors.LabelControl lblTypingIndicator;
    private DevExpress.XtraEditors.PanelControl panelInput;
    private DevExpress.XtraEditors.SimpleButton btnSend;
    private DevExpress.XtraEditors.MemoEdit txtUserInput;
}
