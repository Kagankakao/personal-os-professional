namespace KeganOS.Views;

partial class NotesControl
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
        this.gridControlNotes = new DevExpress.XtraGrid.GridControl();
        this.gridViewNotes = new DevExpress.XtraGrid.Views.Grid.GridView();
        this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
        this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
        this.txtNoteContent = new DevExpress.XtraEditors.MemoEdit();
        this.panelActions = new DevExpress.XtraEditors.PanelControl();
        this.btnNewNote = new DevExpress.XtraEditors.SimpleButton();
        this.btnSaveNote = new DevExpress.XtraEditors.SimpleButton();
        this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
        ((System.ComponentModel.ISupportInitialize)(this.gridControlNotes)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridViewNotes)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtNoteContent.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelActions)).BeginInit();
        this.panelActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
        this.splitContainerControl1.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
        this.splitContainerControl1.Panel2.SuspendLayout();
        this.splitContainerControl1.SuspendLayout();
        this.SuspendLayout();
        // 
        // gridControlNotes
        // 
        this.gridControlNotes.Dock = System.Windows.Forms.DockStyle.Fill;
        this.gridControlNotes.Location = new System.Drawing.Point(0, 0);
        this.gridControlNotes.MainView = this.gridViewNotes;
        this.gridControlNotes.Name = "gridControlNotes";
        this.gridControlNotes.Size = new System.Drawing.Size(200, 390);
        this.gridControlNotes.TabIndex = 0;
        this.gridControlNotes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNotes});
        // 
        // gridViewNotes
        // 
        this.gridViewNotes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTitle,
            this.colDate});
        this.gridViewNotes.GridControl = this.gridControlNotes;
        this.gridViewNotes.Name = "gridViewNotes";
        this.gridViewNotes.OptionsBehavior.Editable = false;
        this.gridViewNotes.OptionsView.ShowGroupPanel = false;
        // 
        // colTitle
        // 
        this.colTitle.Caption = "Title";
        this.colTitle.FieldName = "Title";
        this.colTitle.Name = "colTitle";
        this.colTitle.Visible = true;
        this.colTitle.VisibleIndex = 0;
        // 
        // colDate
        // 
        this.colDate.Caption = "Date";
        this.colDate.FieldName = "CreatedAt";
        this.colDate.Name = "colDate";
        this.colDate.Visible = true;
        this.colDate.VisibleIndex = 1;
        // 
        // txtNoteContent
        // 
        this.txtNoteContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtNoteContent.Location = new System.Drawing.Point(0, 0);
        this.txtNoteContent.Name = "txtNoteContent";
        this.txtNoteContent.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.txtNoteContent.Properties.Appearance.Options.UseFont = true;
        this.txtNoteContent.Size = new System.Drawing.Size(350, 390);
        this.txtNoteContent.TabIndex = 1;
        // 
        // panelActions
        // 
        this.panelActions.Controls.Add(this.btnNewNote);
        this.panelActions.Controls.Add(this.btnSaveNote);
        this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.panelActions.Location = new System.Drawing.Point(20, 410);
        this.panelActions.Name = "panelActions";
        this.panelActions.Size = new System.Drawing.Size(560, 40);
        this.panelActions.TabIndex = 2;
        // 
        // btnNewNote
        // 
        this.btnNewNote.Location = new System.Drawing.Point(5, 5);
        this.btnNewNote.Name = "btnNewNote";
        this.btnNewNote.Size = new System.Drawing.Size(100, 30);
        this.btnNewNote.TabIndex = 0;
        this.btnNewNote.Text = "New Note";
        // 
        // btnSaveNote
        // 
        this.btnSaveNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnSaveNote.Location = new System.Drawing.Point(455, 5);
        this.btnSaveNote.Name = "btnSaveNote";
        this.btnSaveNote.Size = new System.Drawing.Size(100, 30);
        this.btnSaveNote.TabIndex = 1;
        this.btnSaveNote.Text = "Save Note";
        // 
        // splitContainerControl1
        // 
        this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitContainerControl1.Location = new System.Drawing.Point(20, 20);
        this.splitContainerControl1.Name = "splitContainerControl1";
        // 
        // splitContainerControl1.Panel1
        // 
        this.splitContainerControl1.Panel1.Controls.Add(this.gridControlNotes);
        this.splitContainerControl1.Panel1.Text = "Panel1";
        // 
        // splitContainerControl1.Panel2
        // 
        this.splitContainerControl1.Panel2.Controls.Add(this.txtNoteContent);
        this.splitContainerControl1.Panel2.Text = "Panel2";
        this.splitContainerControl1.Size = new System.Drawing.Size(560, 390);
        this.splitContainerControl1.SplitterPosition = 200;
        this.splitContainerControl1.TabIndex = 3;
        // 
        // NotesControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitContainerControl1);
        this.Controls.Add(this.panelActions);
        this.Name = "NotesControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 470);
        ((System.ComponentModel.ISupportInitialize)(this.gridControlNotes)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.gridViewNotes)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtNoteContent.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.panelActions)).EndInit();
        this.panelActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
        this.splitContainerControl1.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
        this.splitContainerControl1.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
        this.splitContainerControl1.ResumeLayout(false);
        this.ResumeLayout(false);

    }

    private DevExpress.XtraGrid.GridControl gridControlNotes;
    private DevExpress.XtraGrid.Views.Grid.GridView gridViewNotes;
    private DevExpress.XtraGrid.Columns.GridColumn colTitle;
    private DevExpress.XtraGrid.Columns.GridColumn colDate;
    private DevExpress.XtraEditors.MemoEdit txtNoteContent;
    private DevExpress.XtraEditors.PanelControl panelActions;
    private DevExpress.XtraEditors.SimpleButton btnNewNote;
    private DevExpress.XtraEditors.SimpleButton btnSaveNote;
    private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
}
