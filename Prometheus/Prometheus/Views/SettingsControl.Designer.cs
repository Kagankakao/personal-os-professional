namespace KeganOS.Views;

partial class SettingsControl
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
        this.scrollContainer = new DevExpress.XtraEditors.XtraScrollableControl();
        
        // KegomoDoro Group
        this.groupKegomoDoro = new DevExpress.XtraEditors.GroupControl();
        this.lblWorkMin = new DevExpress.XtraEditors.LabelControl();
        this.spinWorkMin = new DevExpress.XtraEditors.SpinEdit();
        this.lblShortBreak = new DevExpress.XtraEditors.LabelControl();
        this.spinShortBreak = new DevExpress.XtraEditors.SpinEdit();
        this.lblLongBreak = new DevExpress.XtraEditors.LabelControl();
        this.spinLongBreak = new DevExpress.XtraEditors.SpinEdit();
        this.lblBackColor = new DevExpress.XtraEditors.LabelControl();
        this.colorPickBack = new DevExpress.XtraEditors.ColorPickEdit();
        this.btnSaveSettings = new DevExpress.XtraEditors.SimpleButton();
        this.btnLogout = new DevExpress.XtraEditors.SimpleButton();
        this.btnDeleteUser = new DevExpress.XtraEditors.SimpleButton();
        
        // API Keys Group
        this.groupApiKeys = new DevExpress.XtraEditors.GroupControl();
        this.lblGeminiKey = new DevExpress.XtraEditors.LabelControl();
        this.txtGeminiKey = new DevExpress.XtraEditors.TextEdit();
        this.btnShowCredentials = new DevExpress.XtraEditors.SimpleButton();
        this.lblPixelaUser = new DevExpress.XtraEditors.LabelControl();
        this.txtPixelaUser = new DevExpress.XtraEditors.TextEdit();
        this.lblPixelaToken = new DevExpress.XtraEditors.LabelControl();
        this.txtPixelaToken = new DevExpress.XtraEditors.TextEdit();
        
        // Backup Group
        this.groupBackup = new DevExpress.XtraEditors.GroupControl();
        this.btnCreateBackup = new DevExpress.XtraEditors.SimpleButton();
        this.btnRestoreBackup = new DevExpress.XtraEditors.SimpleButton();
        this.btnOpenBackupFolder = new DevExpress.XtraEditors.SimpleButton();
        this.lblLastBackup = new DevExpress.XtraEditors.LabelControl();
        
        // Bottom Buttons
        this.btnSaveSettings = new DevExpress.XtraEditors.SimpleButton();
        this.btnLogout = new DevExpress.XtraEditors.SimpleButton();
        
        // BeginInit
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
        this.pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.groupKegomoDoro)).BeginInit();
        this.groupKegomoDoro.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.spinWorkMin.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinShortBreak.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinLongBreak.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.colorPickBack.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupApiKeys)).BeginInit();
        this.groupApiKeys.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtGeminiKey.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtPixelaUser.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtPixelaToken.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupBackup)).BeginInit();
        this.groupBackup.SuspendLayout();
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
        this.lblTitle.Appearance.ForeColor = System.Drawing.Color.Black;
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Appearance.Options.UseForeColor = true;
        this.lblTitle.Location = new System.Drawing.Point(0, 5);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(280, 40);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Settings";
        
        // scrollContainer
        this.scrollContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        this.scrollContainer.Location = new System.Drawing.Point(20, 70);
        this.scrollContainer.Name = "scrollContainer";
        this.scrollContainer.Size = new System.Drawing.Size(560, 360);
        this.scrollContainer.TabIndex = 11;
        
        // === KEGOMODORO GROUP ===
        this.groupKegomoDoro.Location = new System.Drawing.Point(0, 0);
        this.groupKegomoDoro.Name = "groupKegomoDoro";
        this.groupKegomoDoro.Size = new System.Drawing.Size(540, 160);
        this.groupKegomoDoro.TabIndex = 0;
        this.groupKegomoDoro.Text = "KegomoDoro Timer";
        this.groupKegomoDoro.Controls.Add(this.lblWorkMin);
        this.groupKegomoDoro.Controls.Add(this.spinWorkMin);
        this.groupKegomoDoro.Controls.Add(this.lblShortBreak);
        this.groupKegomoDoro.Controls.Add(this.spinShortBreak);
        this.groupKegomoDoro.Controls.Add(this.lblLongBreak);
        this.groupKegomoDoro.Controls.Add(this.spinLongBreak);
        
        // lblWorkMin
        this.lblWorkMin.Location = new System.Drawing.Point(20, 35);
        this.lblWorkMin.Name = "lblWorkMin";
        this.lblWorkMin.Text = "Work Duration (Min):";
        
        // spinWorkMin
        this.spinWorkMin.EditValue = new decimal(25);
        this.spinWorkMin.Location = new System.Drawing.Point(160, 32);
        this.spinWorkMin.Name = "spinWorkMin";
        this.spinWorkMin.Size = new System.Drawing.Size(100, 22);
        this.spinWorkMin.Properties.MaxValue = 120;
        this.spinWorkMin.Properties.MinValue = 1;
        
        // lblShortBreak
        this.lblShortBreak.Location = new System.Drawing.Point(20, 65);
        this.lblShortBreak.Name = "lblShortBreak";
        this.lblShortBreak.Text = "Short Break (Min):";
        
        // spinShortBreak
        this.spinShortBreak.EditValue = new decimal(5);
        this.spinShortBreak.Location = new System.Drawing.Point(160, 62);
        this.spinShortBreak.Name = "spinShortBreak";
        this.spinShortBreak.Size = new System.Drawing.Size(100, 22);
        this.spinShortBreak.Properties.MaxValue = 30;
        this.spinShortBreak.Properties.MinValue = 1;
        
        // lblLongBreak
        this.lblLongBreak.Location = new System.Drawing.Point(20, 95);
        this.lblLongBreak.Name = "lblLongBreak";
        this.lblLongBreak.Text = "Long Break (Min):";
        
        // spinLongBreak
        this.spinLongBreak.EditValue = new decimal(20);
        this.spinLongBreak.Location = new System.Drawing.Point(160, 92);
        this.spinLongBreak.Name = "spinLongBreak";
        this.spinLongBreak.Size = new System.Drawing.Size(100, 22);
        this.spinLongBreak.Properties.MaxValue = 60;
        this.spinLongBreak.Properties.MinValue = 1;
        
        
        // === API KEYS GROUP ===
        this.groupApiKeys.Location = new System.Drawing.Point(0, 170);
        this.groupApiKeys.Name = "groupApiKeys";
        this.groupApiKeys.Size = new System.Drawing.Size(540, 130);
        this.groupApiKeys.TabIndex = 1;
        this.groupApiKeys.Text = "API Credentials";
        this.groupApiKeys.Controls.Add(this.lblGeminiKey);
        this.groupApiKeys.Controls.Add(this.txtGeminiKey);
        this.groupApiKeys.Controls.Add(this.btnShowCredentials);
        this.groupApiKeys.Controls.Add(this.lblPixelaUser);
        this.groupApiKeys.Controls.Add(this.txtPixelaUser);
        this.groupApiKeys.Controls.Add(this.lblPixelaToken);
        this.groupApiKeys.Controls.Add(this.txtPixelaToken);
        
        // lblGeminiKey
        this.lblGeminiKey.Location = new System.Drawing.Point(20, 35);
        this.lblGeminiKey.Name = "lblGeminiKey";
        this.lblGeminiKey.Text = "Gemini API Key:";
        
        // txtGeminiKey
        this.txtGeminiKey.Location = new System.Drawing.Point(130, 32);
        this.txtGeminiKey.Name = "txtGeminiKey";
        this.txtGeminiKey.Size = new System.Drawing.Size(280, 22);
        this.txtGeminiKey.Properties.PasswordChar = '●';
        
        // btnShowCredentials
        this.btnShowCredentials.Location = new System.Drawing.Point(420, 30);
        this.btnShowCredentials.Name = "btnShowCredentials";
        this.btnShowCredentials.Size = new System.Drawing.Size(70, 26);
        this.btnShowCredentials.Text = "Show";
        
        // lblPixelaUser
        this.lblPixelaUser.Location = new System.Drawing.Point(20, 65);
        this.lblPixelaUser.Name = "lblPixelaUser";
        this.lblPixelaUser.Text = "Pixela Username:";
        
        // txtPixelaUser
        this.txtPixelaUser.Location = new System.Drawing.Point(130, 62);
        this.txtPixelaUser.Name = "txtPixelaUser";
        this.txtPixelaUser.Size = new System.Drawing.Size(200, 22);
        
        // lblPixelaToken
        this.lblPixelaToken.Location = new System.Drawing.Point(20, 95);
        this.lblPixelaToken.Name = "lblPixelaToken";
        this.lblPixelaToken.Text = "Pixela Token:";
        
        // txtPixelaToken
        this.txtPixelaToken.Location = new System.Drawing.Point(130, 92);
        this.txtPixelaToken.Name = "txtPixelaToken";
        this.txtPixelaToken.Size = new System.Drawing.Size(280, 22);
        this.txtPixelaToken.Properties.PasswordChar = '●';
        
        // === BACKUP GROUP ===
        this.groupBackup.Location = new System.Drawing.Point(0, 310);
        this.groupBackup.Name = "groupBackup";
        this.groupBackup.Size = new System.Drawing.Size(540, 90);
        this.groupBackup.TabIndex = 2;
        this.groupBackup.Text = "Backup & Restore";
        this.groupBackup.Controls.Add(this.btnCreateBackup);
        this.groupBackup.Controls.Add(this.btnCreateBackup);
        this.groupBackup.Controls.Add(this.btnOpenBackupFolder);
        this.groupBackup.Controls.Add(this.lblLastBackup);
        
        // btnCreateBackup
        this.btnCreateBackup.Location = new System.Drawing.Point(20, 35);
        this.btnCreateBackup.Name = "btnCreateBackup";
        this.btnCreateBackup.Size = new System.Drawing.Size(100, 30);
        this.btnCreateBackup.Text = "Create";
        
        
        // btnOpenBackupFolder
        this.btnOpenBackupFolder.Location = new System.Drawing.Point(240, 35);
        this.btnOpenBackupFolder.Name = "btnOpenBackupFolder";
        this.btnOpenBackupFolder.Size = new System.Drawing.Size(100, 30);
        this.btnOpenBackupFolder.Text = "Open Folder";
        
        // lblLastBackup
        this.lblLastBackup.Appearance.ForeColor = System.Drawing.Color.Gray;
        this.lblLastBackup.Appearance.Options.UseForeColor = true;
        this.lblLastBackup.Location = new System.Drawing.Point(350, 42);
        this.lblLastBackup.Name = "lblLastBackup";
        this.lblLastBackup.Text = "Last backup: Never";
        
        // === BUTTONS ===
        // btnSaveSettings
        this.btnSaveSettings.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnSaveSettings.Appearance.Options.UseFont = true;
        this.btnSaveSettings.Location = new System.Drawing.Point(0, 410);
        this.btnSaveSettings.Name = "btnSaveSettings";
        this.btnSaveSettings.Size = new System.Drawing.Size(150, 40);
        this.btnSaveSettings.TabIndex = 3;
        this.btnSaveSettings.Text = "Save All";
        
        // btnLogout
        this.btnLogout.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnLogout.Appearance.ForeColor = System.Drawing.Color.Crimson;
        this.btnLogout.Appearance.Options.UseFont = true;
        this.btnLogout.Appearance.Options.UseForeColor = true;
        this.btnLogout.Location = new System.Drawing.Point(420, 410);
        this.btnLogout.Name = "btnLogout";
        this.btnLogout.Size = new System.Drawing.Size(120, 40);
        this.btnLogout.TabIndex = 4;
        this.btnLogout.Text = "Logout";
        
        // btnDeleteUser
        this.btnDeleteUser.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnDeleteUser.Appearance.ForeColor = System.Drawing.Color.Red;
        this.btnDeleteUser.Appearance.Options.UseFont = true;
        this.btnDeleteUser.Appearance.Options.UseForeColor = true;
        this.btnDeleteUser.Location = new System.Drawing.Point(300, 410);
        this.btnDeleteUser.Name = "btnDeleteUser";
        this.btnDeleteUser.Size = new System.Drawing.Size(110, 40);
        this.btnDeleteUser.TabIndex = 5;
        this.btnDeleteUser.Text = "Delete User";
        
        // Add controls to scrollContainer
        this.scrollContainer.Controls.Add(this.groupKegomoDoro);
        this.scrollContainer.Controls.Add(this.groupApiKeys);
        this.scrollContainer.Controls.Add(this.groupBackup);
        this.scrollContainer.Controls.Add(this.btnSaveSettings);
        this.scrollContainer.Controls.Add(this.btnDeleteUser);
        this.scrollContainer.Controls.Add(this.btnLogout);
        
        // SettingsControl
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.scrollContainer);
        this.Controls.Add(this.pnlHeader);
        this.Name = "SettingsControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 500);
        
        // EndInit
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
        this.pnlHeader.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.spinWorkMin.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinShortBreak.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinLongBreak.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.colorPickBack.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupKegomoDoro)).EndInit();
        this.groupKegomoDoro.ResumeLayout(false);
        this.groupKegomoDoro.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtGeminiKey.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtPixelaUser.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtPixelaToken.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupApiKeys)).EndInit();
        this.groupApiKeys.ResumeLayout(false);
        this.groupApiKeys.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.groupBackup)).EndInit();
        this.groupBackup.ResumeLayout(false);
        this.groupBackup.PerformLayout();
        this.ResumeLayout(false);
    }

    // Header
    private DevExpress.XtraEditors.PanelControl pnlHeader;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraEditors.XtraScrollableControl scrollContainer;
    
    // KegomoDoro
    private DevExpress.XtraEditors.GroupControl groupKegomoDoro;
    private DevExpress.XtraEditors.LabelControl lblWorkMin;
    private DevExpress.XtraEditors.SpinEdit spinWorkMin;
    private DevExpress.XtraEditors.LabelControl lblShortBreak;
    private DevExpress.XtraEditors.SpinEdit spinShortBreak;
    private DevExpress.XtraEditors.LabelControl lblLongBreak;
    private DevExpress.XtraEditors.SpinEdit spinLongBreak;
    private DevExpress.XtraEditors.LabelControl lblBackColor;
    private DevExpress.XtraEditors.ColorPickEdit colorPickBack;
    
    // API Keys
    private DevExpress.XtraEditors.GroupControl groupApiKeys;
    private DevExpress.XtraEditors.LabelControl lblGeminiKey;
    private DevExpress.XtraEditors.TextEdit txtGeminiKey;
    private DevExpress.XtraEditors.SimpleButton btnShowCredentials;
    private DevExpress.XtraEditors.LabelControl lblPixelaUser;
    private DevExpress.XtraEditors.TextEdit txtPixelaUser;
    private DevExpress.XtraEditors.LabelControl lblPixelaToken;
    private DevExpress.XtraEditors.TextEdit txtPixelaToken;
    
    // Backup
    private DevExpress.XtraEditors.GroupControl groupBackup;
    private DevExpress.XtraEditors.SimpleButton btnCreateBackup;
    private DevExpress.XtraEditors.SimpleButton btnRestoreBackup;
    private DevExpress.XtraEditors.SimpleButton btnOpenBackupFolder;
    private DevExpress.XtraEditors.LabelControl lblLastBackup;
    
    // Buttons
    private DevExpress.XtraEditors.SimpleButton btnSaveSettings;
    private DevExpress.XtraEditors.SimpleButton btnLogout;
    private DevExpress.XtraEditors.SimpleButton btnDeleteUser;
}
