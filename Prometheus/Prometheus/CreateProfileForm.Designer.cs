namespace KeganOS
{
    partial class CreateProfileForm
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
            this.groupIdentity = new DevExpress.XtraEditors.GroupControl();
            this.picAvatar = new DevExpress.XtraEditors.PictureEdit();
            this.btnBrowseAvatar = new DevExpress.XtraEditors.SimpleButton();
            this.lblDisplayName = new DevExpress.XtraEditors.LabelControl();
            this.txtDisplayName = new DevExpress.XtraEditors.TextEdit();
            this.lblSymbol = new DevExpress.XtraEditors.LabelControl();
            this.txtSymbol = new DevExpress.XtraEditors.TextEdit();
            
            this.groupJournal = new DevExpress.XtraEditors.GroupControl();
            this.lblJournalPath = new DevExpress.XtraEditors.LabelControl();
            this.btnBrowseJournal = new DevExpress.XtraEditors.SimpleButton();
            
            this.groupPixela = new DevExpress.XtraEditors.GroupControl();
            this.lblPixelaUser = new DevExpress.XtraEditors.LabelControl();
            this.txtPixelaUser = new DevExpress.XtraEditors.TextEdit();
            this.btnCheckPixela = new DevExpress.XtraEditors.SimpleButton();
            this.lblPixelaToken = new DevExpress.XtraEditors.LabelControl();
            this.txtPixelaToken = new DevExpress.XtraEditors.TextEdit();
            this.lblPixelaGraph = new DevExpress.XtraEditors.LabelControl();
            this.txtPixelaGraph = new DevExpress.XtraEditors.TextEdit();
            
            this.groupAI = new DevExpress.XtraEditors.GroupControl();
            this.lblGeminiKey = new DevExpress.XtraEditors.LabelControl();
            this.txtGeminiKey = new DevExpress.XtraEditors.TextEdit();
            
            this.btnCreate = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();

            ((System.ComponentModel.ISupportInitialize)(this.groupIdentity)).BeginInit();
            this.groupIdentity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupJournal)).BeginInit();
            this.groupJournal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupPixela)).BeginInit();
            this.groupPixela.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaUser.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaToken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaGraph.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAI)).BeginInit();
            this.groupAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGeminiKey.Properties)).BeginInit();
            this.SuspendLayout();

            // 
            // groupIdentity
            // 
            this.groupIdentity.Controls.Add(this.picAvatar);
            this.groupIdentity.Controls.Add(this.btnBrowseAvatar);
            this.groupIdentity.Controls.Add(this.lblDisplayName);
            this.groupIdentity.Controls.Add(this.txtDisplayName);
            this.groupIdentity.Controls.Add(this.lblSymbol);
            this.groupIdentity.Controls.Add(this.txtSymbol);
            this.groupIdentity.Location = new System.Drawing.Point(12, 12);
            this.groupIdentity.Name = "groupIdentity";
            this.groupIdentity.Size = new System.Drawing.Size(376, 140);
            this.groupIdentity.TabIndex = 0;
            this.groupIdentity.Text = "Identity";

            // picAvatar
            this.picAvatar.Location = new System.Drawing.Point(10, 35);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            this.picAvatar.Size = new System.Drawing.Size(80, 80);
            this.picAvatar.TabIndex = 0;

            // btnBrowseAvatar
            this.btnBrowseAvatar.Location = new System.Drawing.Point(10, 112);
            this.btnBrowseAvatar.Name = "btnBrowseAvatar";
            this.btnBrowseAvatar.Size = new System.Drawing.Size(80, 23);
            this.btnBrowseAvatar.TabIndex = 1;
            this.btnBrowseAvatar.Text = "Browse...";

            // lblDisplayName
            this.lblDisplayName.Location = new System.Drawing.Point(100, 35);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(64, 13);
            this.lblDisplayName.Text = "Display Name:";

            // txtDisplayName
            this.txtDisplayName.Location = new System.Drawing.Point(100, 50);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(260, 20);
            this.txtDisplayName.TabIndex = 2;

            // lblSymbol
            this.lblSymbol.Location = new System.Drawing.Point(100, 80);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Size = new System.Drawing.Size(76, 13);
            this.lblSymbol.Text = "Personal Symbol:";

            // txtSymbol
            this.txtSymbol.Location = new System.Drawing.Point(100, 95);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(260, 20);
            this.txtSymbol.TabIndex = 3;
            this.txtSymbol.Properties.NullValuePrompt = "🦭";

            // 
            // groupJournal
            // 
            this.groupJournal.Controls.Add(this.lblJournalPath);
            this.groupJournal.Controls.Add(this.btnBrowseJournal);
            this.groupJournal.Location = new System.Drawing.Point(12, 158);
            this.groupJournal.Name = "groupJournal";
            this.groupJournal.Size = new System.Drawing.Size(376, 80);
            this.groupJournal.TabIndex = 1;
            this.groupJournal.Text = "KEGOMODORO Journal";

            // lblJournalPath
            this.lblJournalPath.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblJournalPath.Location = new System.Drawing.Point(10, 40);
            this.lblJournalPath.Name = "lblJournalPath";
            this.lblJournalPath.Size = new System.Drawing.Size(240, 13);
            this.lblJournalPath.Text = "Auto-generated from name";

            // btnBrowseJournal
            this.btnBrowseJournal.Location = new System.Drawing.Point(280, 35);
            this.btnBrowseJournal.Name = "btnBrowseJournal";
            this.btnBrowseJournal.Size = new System.Drawing.Size(80, 23);
            this.btnBrowseJournal.TabIndex = 0;
            this.btnBrowseJournal.Text = "Link File...";

            // 
            // groupPixela
            // 
            this.groupPixela.Controls.Add(this.lblPixelaUser);
            this.groupPixela.Controls.Add(this.txtPixelaUser);
            this.groupPixela.Controls.Add(this.btnCheckPixela);
            this.groupPixela.Controls.Add(this.lblPixelaToken);
            this.groupPixela.Controls.Add(this.txtPixelaToken);
            this.groupPixela.Controls.Add(this.lblPixelaGraph);
            this.groupPixela.Controls.Add(this.txtPixelaGraph);
            this.groupPixela.Location = new System.Drawing.Point(12, 244);
            this.groupPixela.Name = "groupPixela";
            this.groupPixela.Size = new System.Drawing.Size(376, 120);
            this.groupPixela.TabIndex = 2;
            this.groupPixela.Text = "Pixe.la (Optional)";

            // lblPixelaUser
            this.lblPixelaUser.Location = new System.Drawing.Point(10, 30);
            this.lblPixelaUser.Name = "lblPixelaUser";
            this.lblPixelaUser.Size = new System.Drawing.Size(52, 13);
            this.lblPixelaUser.Text = "Username:";

            // txtPixelaUser
            this.txtPixelaUser.Location = new System.Drawing.Point(70, 27);
            this.txtPixelaUser.Name = "txtPixelaUser";
            this.txtPixelaUser.Size = new System.Drawing.Size(200, 20);
            this.txtPixelaUser.TabIndex = 0;

            // btnCheckPixela
            this.btnCheckPixela.Location = new System.Drawing.Point(280, 25);
            this.btnCheckPixela.Name = "btnCheckPixela";
            this.btnCheckPixela.Size = new System.Drawing.Size(80, 23);
            this.btnCheckPixela.TabIndex = 1;
            this.btnCheckPixela.Text = "Check";

            // lblPixelaToken
            this.lblPixelaToken.Location = new System.Drawing.Point(10, 60);
            this.lblPixelaToken.Name = "lblPixelaToken";
            this.lblPixelaToken.Size = new System.Drawing.Size(33, 13);
            this.lblPixelaToken.Text = "Token:";

            // txtPixelaToken
            this.txtPixelaToken.Location = new System.Drawing.Point(70, 57);
            this.txtPixelaToken.Name = "txtPixelaToken";
            this.txtPixelaToken.Properties.PasswordChar = '*';
            this.txtPixelaToken.Size = new System.Drawing.Size(290, 20);
            this.txtPixelaToken.TabIndex = 2;

            // lblPixelaGraph
            this.lblPixelaGraph.Location = new System.Drawing.Point(10, 90);
            this.lblPixelaGraph.Name = "lblPixelaGraph";
            this.lblPixelaGraph.Size = new System.Drawing.Size(47, 13);
            this.lblPixelaGraph.Text = "Graph ID:";

            // txtPixelaGraph
            this.txtPixelaGraph.Location = new System.Drawing.Point(70, 87);
            this.txtPixelaGraph.Name = "txtPixelaGraph";
            this.txtPixelaGraph.Size = new System.Drawing.Size(290, 20);
            this.txtPixelaGraph.TabIndex = 3;
            this.txtPixelaGraph.Properties.NullValuePrompt = "focus";

            // 
            // groupAI
            // 
            this.groupAI.Controls.Add(this.lblGeminiKey);
            this.groupAI.Controls.Add(this.txtGeminiKey);
            this.groupAI.Location = new System.Drawing.Point(12, 370);
            this.groupAI.Name = "groupAI";
            this.groupAI.Size = new System.Drawing.Size(376, 70);
            this.groupAI.TabIndex = 3;
            this.groupAI.Text = "AI Integration (Optional)";

            // lblGeminiKey
            this.lblGeminiKey.Location = new System.Drawing.Point(10, 35);
            this.lblGeminiKey.Name = "lblGeminiKey";
            this.lblGeminiKey.Size = new System.Drawing.Size(76, 13);
            this.lblGeminiKey.Text = "Gemini API Key:";

            // txtGeminiKey
            this.txtGeminiKey.Location = new System.Drawing.Point(100, 32);
            this.txtGeminiKey.Name = "txtGeminiKey";
            this.txtGeminiKey.Properties.PasswordChar = '*';
            this.txtGeminiKey.Size = new System.Drawing.Size(260, 20);
            this.txtGeminiKey.TabIndex = 0;

            // 
            // btnCreate
            // 
            this.btnCreate.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreate.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
            this.btnCreate.Location = new System.Drawing.Point(212, 450);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(85, 30);
            this.btnCreate.TabIndex = 4;
            this.btnCreate.Text = "Create";

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(303, 450);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(85, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";

            // 
            // CreateProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 495);
            this.Controls.Add(this.groupIdentity);
            this.Controls.Add(this.groupJournal);
            this.Controls.Add(this.groupPixela);
            this.Controls.Add(this.groupAI);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateProfileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Professional Profile";

            ((System.ComponentModel.ISupportInitialize)(this.groupIdentity)).EndInit();
            this.groupIdentity.ResumeLayout(false);
            this.groupIdentity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDisplayName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupJournal)).EndInit();
            this.groupJournal.ResumeLayout(false);
            this.groupJournal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupPixela)).EndInit();
            this.groupPixela.ResumeLayout(false);
            this.groupPixela.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaUser.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaToken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPixelaGraph.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupAI)).EndInit();
            this.groupAI.ResumeLayout(false);
            this.groupAI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtGeminiKey.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        private DevExpress.XtraEditors.GroupControl groupIdentity;
        private DevExpress.XtraEditors.PictureEdit picAvatar;
        private DevExpress.XtraEditors.SimpleButton btnBrowseAvatar;
        private DevExpress.XtraEditors.LabelControl lblDisplayName;
        private DevExpress.XtraEditors.TextEdit txtDisplayName;
        private DevExpress.XtraEditors.LabelControl lblSymbol;
        private DevExpress.XtraEditors.TextEdit txtSymbol;
        
        private DevExpress.XtraEditors.GroupControl groupJournal;
        private DevExpress.XtraEditors.LabelControl lblJournalPath;
        private DevExpress.XtraEditors.SimpleButton btnBrowseJournal;
        
        private DevExpress.XtraEditors.GroupControl groupPixela;
        private DevExpress.XtraEditors.LabelControl lblPixelaUser;
        private DevExpress.XtraEditors.TextEdit txtPixelaUser;
        private DevExpress.XtraEditors.SimpleButton btnCheckPixela;
        private DevExpress.XtraEditors.LabelControl lblPixelaToken;
        private DevExpress.XtraEditors.TextEdit txtPixelaToken;
        private DevExpress.XtraEditors.LabelControl lblPixelaGraph;
        private DevExpress.XtraEditors.TextEdit txtPixelaGraph;
        
        private DevExpress.XtraEditors.GroupControl groupAI;
        private DevExpress.XtraEditors.LabelControl lblGeminiKey;
        private DevExpress.XtraEditors.TextEdit txtGeminiKey;
        
        private DevExpress.XtraEditors.SimpleButton btnCreate;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
    }
}
