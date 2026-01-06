namespace KeganOS.Views;

partial class JournalControl
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
        this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
        this.pnlTimeline = new DevExpress.XtraEditors.GroupControl();
        this.listEntries = new DevExpress.XtraEditors.ListBoxControl();
        this.panelEdit = new DevExpress.XtraEditors.PanelControl();
        this.lblTodayDate = new DevExpress.XtraEditors.LabelControl();
        this.txtEntry = new DevExpress.XtraEditors.MemoEdit();
        this.pnlEditFooter = new DevExpress.XtraEditors.PanelControl();
        this.btnOpenNotepad = new DevExpress.XtraEditors.SimpleButton();
        this.btnSave = new DevExpress.XtraEditors.SimpleButton();
        this.lblFocusMetric = new DevExpress.XtraEditors.LabelControl();
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
        this.pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
        this.splitContainerControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
        this.splitContainerControl1.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
        this.splitContainerControl1.Panel2.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pnlTimeline)).BeginInit();
        this.pnlTimeline.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.listEntries)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).BeginInit();
        this.panelEdit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtEntry.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlEditFooter)).BeginInit();
        this.pnlEditFooter.SuspendLayout();
        this.SuspendLayout();
        // 
        // pnlHeader
        this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlHeader.Controls.Add(this.lblTitle);
        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlHeader.Location = new System.Drawing.Point(20, 20);
        this.pnlHeader.Name = "pnlHeader";
        this.pnlHeader.Size = new System.Drawing.Size(760, 50);
        this.pnlHeader.TabIndex = 1;
        
        // lblTitle
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 22F);
        this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Appearance.Options.UseForeColor = true;
        this.lblTitle.Location = new System.Drawing.Point(0, 5);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(180, 40);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Daily Journal";

        // splitContainerControl1
        this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainerControl1.Location = new System.Drawing.Point(20, 70);
        this.splitContainerControl1.Name = "splitContainerControl1";
        
        // Panel 1 (Timeline)
        this.splitContainerControl1.Panel1.Controls.Add(this.pnlTimeline);
        this.splitContainerControl1.Panel1.Text = "Timeline";
        
        // Panel 2 (Composition)
        this.splitContainerControl1.Panel2.Controls.Add(this.panelEdit);
        this.splitContainerControl1.Panel2.Text = "Composition";
        this.splitContainerControl1.Size = new System.Drawing.Size(760, 360);
        this.splitContainerControl1.SplitterPosition = 280;
        this.splitContainerControl1.TabIndex = 0;
        // 
        // pnlTimeline
        this.pnlTimeline.Controls.Add(this.listEntries);
        this.pnlTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTimeline.Location = new System.Drawing.Point(0, 0);
        this.pnlTimeline.Name = "pnlTimeline";
        this.pnlTimeline.Size = new System.Drawing.Size(280, 360);
        this.pnlTimeline.TabIndex = 0;
        this.pnlTimeline.Text = "History";
        
        // listEntries
        this.listEntries.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.listEntries.Dock = System.Windows.Forms.DockStyle.Fill;
        this.listEntries.ItemHeight = 110;
        this.listEntries.Location = new System.Drawing.Point(2, 25);
        this.listEntries.Name = "listEntries";
        this.listEntries.Size = new System.Drawing.Size(276, 333);
        this.listEntries.TabIndex = 0;
        this.listEntries.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
        this.listEntries.Appearance.Options.UseBackColor = true;
        // 
        // panelEdit
        this.panelEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.panelEdit.Controls.Add(this.txtEntry);
        this.panelEdit.Controls.Add(this.lblTodayDate);
        this.panelEdit.Controls.Add(this.pnlEditFooter);
        this.panelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelEdit.Location = new System.Drawing.Point(0, 0);
        this.panelEdit.Name = "panelEdit";
        this.panelEdit.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
        this.panelEdit.Size = new System.Drawing.Size(470, 360);
        this.panelEdit.TabIndex = 0;

        // lblTodayDate
        this.lblTodayDate.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
        this.lblTodayDate.Appearance.ForeColor = System.Drawing.Color.Gray;
        this.lblTodayDate.Appearance.Options.UseFont = true;
        this.lblTodayDate.Appearance.Options.UseForeColor = true;
        this.lblTodayDate.Location = new System.Drawing.Point(15, 10);
        this.lblTodayDate.Name = "lblTodayDate";
        this.lblTodayDate.Size = new System.Drawing.Size(140, 21);
        this.lblTodayDate.TabIndex = 0;
        this.lblTodayDate.Text = "Log your journey:";

        // txtEntry
        this.txtEntry.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtEntry.Location = new System.Drawing.Point(15, 40);
        this.txtEntry.Name = "txtEntry";
        this.txtEntry.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 16F);
        this.txtEntry.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(32, 33, 36);
        this.txtEntry.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(250, 250, 255);
        this.txtEntry.Properties.Appearance.Options.UseFont = true;
        this.txtEntry.Properties.Appearance.Options.UseForeColor = true;
        this.txtEntry.Properties.Appearance.Options.UseBackColor = true;
        this.txtEntry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.txtEntry.Properties.NullValuePrompt = "What defines your day today?";
        this.txtEntry.Properties.NullValuePromptShowForEmptyValue = true;
        this.txtEntry.Size = new System.Drawing.Size(440, 270);
        this.txtEntry.TabIndex = 1;

        // pnlEditFooter
        this.pnlEditFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlEditFooter.Controls.Add(this.lblFocusMetric);
        this.pnlEditFooter.Controls.Add(this.btnOpenNotepad);
        this.pnlEditFooter.Controls.Add(this.btnSave);
        this.pnlEditFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlEditFooter.Location = new System.Drawing.Point(15, 310);
        this.pnlEditFooter.Name = "pnlEditFooter";
        this.pnlEditFooter.Size = new System.Drawing.Size(440, 50);
        this.pnlEditFooter.TabIndex = 2;

        // lblFocusMetric
        this.lblFocusMetric.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblFocusMetric.Appearance.ForeColor = System.Drawing.Color.Cyan;
        this.lblFocusMetric.Appearance.Options.UseFont = true;
        this.lblFocusMetric.Appearance.Options.UseForeColor = true;
        this.lblFocusMetric.Location = new System.Drawing.Point(0, 15);
        this.lblFocusMetric.Name = "lblFocusMetric";
        this.lblFocusMetric.Size = new System.Drawing.Size(120, 15);
        this.lblFocusMetric.TabIndex = 4;
        this.lblFocusMetric.Text = "FOCUS: 00:00:00";
        // 
        // btnOpenNotepad
        this.btnOpenNotepad.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnOpenNotepad.Location = new System.Drawing.Point(210, 10);
        this.btnOpenNotepad.Name = "btnOpenNotepad";
        this.btnOpenNotepad.Size = new System.Drawing.Size(100, 30);
        this.btnOpenNotepad.TabIndex = 3;
        this.btnOpenNotepad.Text = "Raw View";
        this.btnOpenNotepad.Click += new System.EventHandler(this.BtnOpenNotepad_Click);
        
        // btnSave
        this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnSave.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.btnSave.Appearance.Options.UseFont = true;
        this.btnSave.Appearance.Options.UseForeColor = true;
        this.btnSave.Location = new System.Drawing.Point(320, 10);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(120, 30);
        this.btnSave.TabIndex = 2;
        this.btnSave.Text = "Add to Journal";
        this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

        // JournalControl
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitContainerControl1);
        this.Controls.Add(this.pnlHeader);
        this.Name = "JournalControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(800, 450);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
        this.splitContainerControl1.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
        this.splitContainerControl1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
        this.splitContainerControl1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).EndInit();
        this.panelEdit.ResumeLayout(false);
        this.panelEdit.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtEntry.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraEditors.PanelControl pnlHeader;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    private DevExpress.XtraEditors.GroupControl pnlTimeline;
    private DevExpress.XtraEditors.ListBoxControl listEntries;
    private DevExpress.XtraEditors.PanelControl panelEdit;
    private DevExpress.XtraEditors.LabelControl lblTodayDate;
    private DevExpress.XtraEditors.MemoEdit txtEntry;
    private DevExpress.XtraEditors.PanelControl pnlEditFooter;
    private DevExpress.XtraEditors.LabelControl lblFocusMetric;
    private DevExpress.XtraEditors.SimpleButton btnSave;
    private DevExpress.XtraEditors.SimpleButton btnOpenNotepad;
}
