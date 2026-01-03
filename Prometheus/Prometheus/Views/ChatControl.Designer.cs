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
        this.txtChatHistory = new DevExpress.XtraEditors.MemoEdit();
        this.panelInput = new DevExpress.XtraEditors.PanelControl();
        this.btnSend = new DevExpress.XtraEditors.SimpleButton();
        this.txtUserInput = new DevExpress.XtraEditors.TextEdit();
        ((System.ComponentModel.ISupportInitialize)(this.txtChatHistory.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelInput)).BeginInit();
        this.panelInput.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtUserInput.Properties)).BeginInit();
        this.SuspendLayout();
        // 
        // txtChatHistory
        // 
        this.txtChatHistory.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtChatHistory.Location = new System.Drawing.Point(20, 20);
        this.txtChatHistory.Name = "txtChatHistory";
        this.txtChatHistory.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.txtChatHistory.Properties.Appearance.Options.UseFont = true;
        this.txtChatHistory.Properties.ReadOnly = true;
        this.txtChatHistory.Size = new System.Drawing.Size(560, 350);
        this.txtChatHistory.TabIndex = 0;
        // 
        // panelInput
        // 
        this.panelInput.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.panelInput.Controls.Add(this.btnSend);
        this.panelInput.Controls.Add(this.txtUserInput);
        this.panelInput.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelInput.Location = new System.Drawing.Point(20, 370);
        this.panelInput.Name = "panelInput";
        this.panelInput.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
        this.panelInput.Size = new System.Drawing.Size(560, 60);
        this.panelInput.TabIndex = 1;
        // 
        // btnSend
        // 
        this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnSend.Location = new System.Drawing.Point(480, 10);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new System.Drawing.Size(80, 50);
        this.btnSend.TabIndex = 1;
        this.btnSend.Text = "Consult";
        // 
        // txtUserInput
        // 
        this.txtUserInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
        this.txtUserInput.Location = new System.Drawing.Point(0, 10);
        this.txtUserInput.Name = "txtUserInput";
        this.txtUserInput.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.txtUserInput.Properties.Appearance.Options.UseFont = true;
        this.txtUserInput.Properties.AutoHeight = false;
        this.txtUserInput.Size = new System.Drawing.Size(470, 50);
        this.txtUserInput.TabIndex = 0;
        // 
        // ChatControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.txtChatHistory);
        this.Controls.Add(this.panelInput);
        this.Name = "ChatControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        ((System.ComponentModel.ISupportInitialize)(this.txtChatHistory.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelInput)).EndInit();
        this.panelInput.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.txtUserInput.Properties)).EndInit();
        this.ResumeLayout(false);

    }

    private DevExpress.XtraEditors.MemoEdit txtChatHistory;
    private DevExpress.XtraEditors.PanelControl panelInput;
    private DevExpress.XtraEditors.SimpleButton btnSend;
    private DevExpress.XtraEditors.TextEdit txtUserInput;
}
