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
        this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
        this.gridControlEntries = new DevExpress.XtraGrid.GridControl();
        this.gridViewEntries = new DevExpress.XtraGrid.Views.Grid.GridView();
        this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
        this.colNote = new DevExpress.XtraGrid.Columns.GridColumn();
        this.panelEdit = new DevExpress.XtraEditors.PanelControl();
        this.btnOpenNotepad = new DevExpress.XtraEditors.SimpleButton();
        this.btnSave = new DevExpress.XtraEditors.SimpleButton();
        this.txtEntry = new DevExpress.XtraEditors.MemoEdit();
        this.lblTodayDate = new DevExpress.XtraEditors.LabelControl();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
        this.splitContainerControl1.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
        this.splitContainerControl1.Panel2.SuspendLayout();
        this.splitContainerControl1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.gridControlEntries)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridViewEntries)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).BeginInit();
        this.panelEdit.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtEntry.Properties)).BeginInit();
        this.SuspendLayout();
        // 
        // splitContainerControl1
        // 
        this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainerControl1.Location = new System.Drawing.Point(20, 62);
        this.splitContainerControl1.Name = "splitContainerControl1";
        // 
        // splitContainerControl1.Panel1
        // 
        this.splitContainerControl1.Panel1.Controls.Add(this.gridControlEntries);
        this.splitContainerControl1.Panel1.Text = "Panel1";
        // 
        // splitContainerControl1.Panel2
        // 
        this.splitContainerControl1.Panel2.Controls.Add(this.panelEdit);
        this.splitContainerControl1.Panel2.Text = "Panel2";
        this.splitContainerControl1.Size = new System.Drawing.Size(760, 368);
        this.splitContainerControl1.SplitterPosition = 250;
        this.splitContainerControl1.TabIndex = 0;
        // 
        // gridControlEntries
        // 
        this.gridControlEntries.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gridControlEntries.Location = new System.Drawing.Point(0, 0);
        this.gridControlEntries.MainView = this.gridViewEntries;
        this.gridControlEntries.Name = "gridControlEntries";
        this.gridControlEntries.Size = new System.Drawing.Size(250, 368);
        this.gridControlEntries.TabIndex = 0;
        this.gridControlEntries.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewEntries});
        // 
        // gridViewEntries
        // 
        this.gridViewEntries.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDate,
            this.colNote});
        this.gridViewEntries.GridControl = this.gridControlEntries;
        this.gridViewEntries.Name = "gridViewEntries";
        this.gridViewEntries.OptionsBehavior.Editable = false;
        this.gridViewEntries.OptionsView.ShowGroupPanel = false;
        this.gridViewEntries.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewEntries_FocusedRowChanged);
        // 
        // colDate
        // 
        this.colDate.Caption = "Date";
        this.colDate.DisplayFormat.FormatString = "yyyy-MM-dd";
        this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        this.colDate.FieldName = "Date";
        this.colDate.Name = "colDate";
        this.colDate.Visible = true;
        this.colDate.VisibleIndex = 0;
        this.colDate.Width = 80;
        // 
        // colNote
        // 
        this.colNote.Caption = "Preview";
        this.colNote.FieldName = "NoteText";
        this.colNote.Name = "colNote";
        this.colNote.Visible = true;
        this.colNote.VisibleIndex = 1;
        this.colNote.Width = 152;
        // 
        // panelEdit
        // 
        this.panelEdit.Controls.Add(this.btnOpenNotepad);
        this.panelEdit.Controls.Add(this.btnSave);
        this.panelEdit.Controls.Add(this.txtEntry);
        this.panelEdit.Controls.Add(this.lblTodayDate);
        this.panelEdit.Dock = System.Windows.Forms.DockStyle.Fill;
        this.panelEdit.Location = new System.Drawing.Point(0, 0);
        this.panelEdit.Name = "panelEdit";
        this.panelEdit.Padding = new System.Windows.Forms.Padding(10);
        this.panelEdit.Size = new System.Drawing.Size(500, 368);
        this.panelEdit.TabIndex = 0;
        // 
        // btnOpenNotepad
        // 
        this.btnOpenNotepad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOpenNotepad.Location = new System.Drawing.Point(264, 330);
        this.btnOpenNotepad.Name = "btnOpenNotepad";
        this.btnOpenNotepad.Size = new System.Drawing.Size(110, 28);
        this.btnOpenNotepad.TabIndex = 3;
        this.btnOpenNotepad.Text = "Open in Notepad";
        this.btnOpenNotepad.Click += new System.EventHandler(this.BtnOpenNotepad_Click);
        // 
        // btnSave
        // 
        this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        this.btnSave.Appearance.Options.UseFont = true;
        this.btnSave.Location = new System.Drawing.Point(380, 330);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(110, 28);
        this.btnSave.TabIndex = 2;
        this.btnSave.Text = "Add to Journal";
        this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
        // 
        // txtEntry
        // 
        this.txtEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
        this.txtEntry.Location = new System.Drawing.Point(10, 40);
        this.txtEntry.Name = "txtEntry";
        this.txtEntry.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11.25F);
        this.txtEntry.Properties.Appearance.Options.UseFont = true;
        this.txtEntry.Size = new System.Drawing.Size(480, 280);
        this.txtEntry.TabIndex = 1;
        // 
        // lblTodayDate
        // 
        this.lblTodayDate.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        this.lblTodayDate.Appearance.Options.UseFont = true;
        this.lblTodayDate.Location = new System.Drawing.Point(13, 13);
        this.lblTodayDate.Name = "lblTodayDate";
        this.lblTodayDate.Size = new System.Drawing.Size(149, 21);
        this.lblTodayDate.TabIndex = 0;
        this.lblTodayDate.Text = "Today's Log Entry:";
        // 
        // lblTitle
        // 
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Location = new System.Drawing.Point(20, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
        this.lblTitle.Size = new System.Drawing.Size(164, 42);
        this.lblTitle.TabIndex = 1;
        this.lblTitle.Text = "Daily Journal";
        // 
        // JournalControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitContainerControl1);
        this.Controls.Add(this.lblTitle);
        this.Name = "JournalControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(800, 450);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
        this.splitContainerControl1.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
        this.splitContainerControl1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
        this.splitContainerControl1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.gridControlEntries)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridViewEntries)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelEdit)).EndInit();
        this.panelEdit.ResumeLayout(false);
        this.panelEdit.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtEntry.Properties)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    private DevExpress.XtraGrid.GridControl gridControlEntries;
    private DevExpress.XtraGrid.Views.Grid.GridView gridViewEntries;
    private DevExpress.XtraEditors.PanelControl panelEdit;
    private DevExpress.XtraEditors.SimpleButton btnSave;
    private DevExpress.XtraEditors.MemoEdit txtEntry;
    private DevExpress.XtraEditors.LabelControl lblTodayDate;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraGrid.Columns.GridColumn colDate;
    private DevExpress.XtraGrid.Columns.GridColumn colNote;
    private DevExpress.XtraEditors.SimpleButton btnOpenNotepad;
}
