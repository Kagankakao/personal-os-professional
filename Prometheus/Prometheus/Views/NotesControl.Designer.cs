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
        // Initializations
        this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        this.txtSearch = new DevExpress.XtraEditors.SearchControl();
        this.btnNewNote = new DevExpress.XtraEditors.SimpleButton();
        this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
        this.pnlNotesList = new DevExpress.XtraEditors.GroupControl();
        this.listNotes = new DevExpress.XtraEditors.ListBoxControl();
        this.pnlEditor = new DevExpress.XtraEditors.GroupControl();
        this.txtTitle = new DevExpress.XtraEditors.TextEdit();
        this.txtContent = new DevExpress.XtraEditors.MemoEdit();
        this.pnlImages = new System.Windows.Forms.FlowLayoutPanel();
        this.lblDropHint = new System.Windows.Forms.Label();
        this.pnlEditorFooter = new DevExpress.XtraEditors.PanelControl();
        this.lblWordCount = new DevExpress.XtraEditors.LabelControl();
        this.lblLastSaved = new DevExpress.XtraEditors.LabelControl();
        this.btnSave = new DevExpress.XtraEditors.SimpleButton();
        this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
        
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
        this.pnlHeader.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pnlNotesList)).BeginInit();
        this.pnlNotesList.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.listNotes)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlEditor)).BeginInit();
        this.pnlEditor.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlEditorFooter)).BeginInit();
        this.pnlEditorFooter.SuspendLayout();
        this.SuspendLayout();
        
        // ════════════════════════════════════════════════════════════════════
        // HEADER
        // ════════════════════════════════════════════════════════════════════
        this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlHeader.Location = new System.Drawing.Point(15, 15);
        this.pnlHeader.Name = "pnlHeader";
        this.pnlHeader.Size = new System.Drawing.Size(950, 50);
        this.pnlHeader.TabIndex = 0;
        this.pnlHeader.Controls.Add(this.lblTitle);
        this.pnlHeader.Controls.Add(this.txtSearch);
        this.pnlHeader.Controls.Add(this.btnNewNote);
        
        // Title
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semilight", 22F);
        this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Appearance.Options.UseForeColor = true;
        this.lblTitle.Location = new System.Drawing.Point(0, 5);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Size = new System.Drawing.Size(200, 40);
        this.lblTitle.TabIndex = 0;
        this.lblTitle.Text = "Neural Notes";
        
        // Search
        this.txtSearch.Location = new System.Drawing.Point(200, 10);
        this.txtSearch.Name = "txtSearch";
        this.txtSearch.Properties.NullValuePrompt = "Search notes...";
        this.txtSearch.Properties.ShowClearButton = true;
        this.txtSearch.Size = new System.Drawing.Size(300, 28);
        this.txtSearch.TabIndex = 1;
        
        // New Note Button
        this.btnNewNote.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnNewNote.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 255, 127); // Spring Green
        this.btnNewNote.Appearance.Options.UseFont = true;
        this.btnNewNote.Appearance.Options.UseForeColor = true;
        this.btnNewNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnNewNote.Location = new System.Drawing.Point(820, 8);
        this.btnNewNote.Name = "btnNewNote";
        this.btnNewNote.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
        this.btnNewNote.Size = new System.Drawing.Size(130, 32);
        this.btnNewNote.TabIndex = 2;
        this.btnNewNote.Text = "+ CREATE NEW";
        
        // ════════════════════════════════════════════════════════════════════
        // MAIN SPLIT
        // ════════════════════════════════════════════════════════════════════
        this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel1;
        this.splitMain.Location = new System.Drawing.Point(15, 65);
        this.splitMain.Name = "splitMain";
        this.splitMain.Panel1.Controls.Add(this.pnlNotesList);
        this.splitMain.Panel2.Controls.Add(this.pnlEditor);
        this.splitMain.Size = new System.Drawing.Size(950, 500);
        this.splitMain.SplitterPosition = 280;
        this.splitMain.TabIndex = 1;
        
        // ════════════════════════════════════════════════════════════════════
        // LEFT - NOTES LIST
        // ════════════════════════════════════════════════════════════════════
        this.pnlNotesList.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlNotesList.Location = new System.Drawing.Point(0, 0);
        this.pnlNotesList.Name = "pnlNotesList";
        this.pnlNotesList.Size = new System.Drawing.Size(280, 500);
        this.pnlNotesList.TabIndex = 0;
        this.pnlNotesList.Text = "Notes";
        this.pnlNotesList.Controls.Add(this.listNotes);
        
        // Notes List
        this.listNotes.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.listNotes.Dock = System.Windows.Forms.DockStyle.Fill;
        this.listNotes.ItemHeight = 90;
        this.listNotes.Location = new System.Drawing.Point(2, 25);
        this.listNotes.Name = "listNotes";
        this.listNotes.SelectionMode = System.Windows.Forms.SelectionMode.One;
        this.listNotes.Size = new System.Drawing.Size(276, 473);
        this.listNotes.TabIndex = 0;
        this.listNotes.Appearance.BackColor = System.Drawing.Color.Transparent;
        this.listNotes.Appearance.Options.UseBackColor = true;
        
        // ════════════════════════════════════════════════════════════════════
        // RIGHT - EDITOR
        // ════════════════════════════════════════════════════════════════════
        this.pnlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlEditor.Location = new System.Drawing.Point(0, 0);
        this.pnlEditor.Name = "pnlEditor";
        this.pnlEditor.Size = new System.Drawing.Size(660, 500);
        this.pnlEditor.TabIndex = 0;
        this.pnlEditor.Text = "Note Editor";
        
        this.pnlEditor.Controls.Add(this.pnlEditorFooter); // Bottom
        this.pnlEditor.Controls.Add(this.txtTitle);        // Top
        this.pnlEditor.Controls.Add(this.pnlImages);       // Top (Collapsible gallery)
        this.pnlEditor.Controls.Add(this.txtContent);      // Fill
        
        this.pnlEditorFooter.BringToFront();
        this.txtTitle.BringToFront();
        this.pnlImages.BringToFront();
        this.txtContent.SendToBack();
        
        // Title Input
        this.txtTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.txtTitle.Location = new System.Drawing.Point(2, 25);
        this.txtTitle.Name = "txtTitle";
        this.txtTitle.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 24F);
        this.txtTitle.Properties.Appearance.Options.UseFont = true;
        this.txtTitle.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.txtTitle.Properties.NullValuePrompt = "Deep thoughts begin here...";
        this.txtTitle.Properties.NullValuePromptShowForEmptyValue = true;
        this.txtTitle.Size = new System.Drawing.Size(656, 50);
        this.txtTitle.TabIndex = 0;
        
        // Content
        this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtContent.Location = new System.Drawing.Point(2, 75);
        this.txtContent.Name = "txtContent";
        this.txtContent.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.txtContent.Properties.Appearance.Options.UseFont = true;
        this.txtContent.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.txtContent.Properties.NullValuePrompt = "The mind is like a parachute. It doesn't work if it isn't open...";
        this.txtContent.Properties.NullValuePromptShowForEmptyValue = true;
        this.txtContent.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtContent.Size = new System.Drawing.Size(656, 300);
        this.txtContent.TabIndex = 1;
        this.txtContent.AllowDrop = true;
        
        // Images Panel (Gallery - Visible only if has images)
        this.pnlImages.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlImages.Location = new System.Drawing.Point(2, 61);
        this.pnlImages.Name = "pnlImages";
        this.pnlImages.Size = new System.Drawing.Size(656, 90);
        this.pnlImages.MinimumSize = new System.Drawing.Size(0, 0);
        this.pnlImages.AutoScroll = true;
        this.pnlImages.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
        this.pnlImages.AllowDrop = true;
        this.pnlImages.BorderStyle = System.Windows.Forms.BorderStyle.None;
        this.pnlImages.BackColor = System.Drawing.Color.FromArgb(35, 35, 45); // Subtle separator bg
        this.pnlImages.Visible = false;
        
        // Add Image Action in Footer
        this.lblDropHint.Text = "📷 Add Image";
        this.lblDropHint.ForeColor = System.Drawing.Color.FromArgb(180, 180, 180);
        this.lblDropHint.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.lblDropHint.AutoSize = true;
        this.lblDropHint.Location = new System.Drawing.Point(260, 18);
        this.pnlEditorFooter.Controls.Add(this.lblDropHint);
        
        // Editor Footer
        this.pnlEditorFooter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlEditorFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlEditorFooter.Location = new System.Drawing.Point(2, 448);
        this.pnlEditorFooter.Name = "pnlEditorFooter";
        this.pnlEditorFooter.Size = new System.Drawing.Size(656, 50);
        this.pnlEditorFooter.TabIndex = 2;
        this.pnlEditorFooter.Controls.Add(this.lblWordCount);
        this.pnlEditorFooter.Controls.Add(this.lblLastSaved);
        this.pnlEditorFooter.Controls.Add(this.btnSave);
        this.pnlEditorFooter.Controls.Add(this.btnDelete);
        
        // Word Count
        this.lblWordCount.Appearance.ForeColor = System.Drawing.Color.Gray;
        this.lblWordCount.Appearance.Options.UseForeColor = true;
        this.lblWordCount.Location = new System.Drawing.Point(10, 18);
        this.lblWordCount.Name = "lblWordCount";
        this.lblWordCount.Size = new System.Drawing.Size(100, 15);
        this.lblWordCount.Text = "0 words • 0 chars";
        
        // Last Saved
        this.lblLastSaved.Appearance.ForeColor = System.Drawing.Color.Gray;
        this.lblLastSaved.Appearance.Options.UseForeColor = true;
        this.lblLastSaved.Location = new System.Drawing.Point(130, 18);
        this.lblLastSaved.Name = "lblLastSaved";
        this.lblLastSaved.Size = new System.Drawing.Size(100, 15);
        this.lblLastSaved.Text = "Unsaved";
        
        // Delete Button
        this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnDelete.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.btnDelete.Appearance.ForeColor = System.Drawing.Color.FromArgb(244, 67, 54);
        this.btnDelete.Appearance.Options.UseFont = true;
        this.btnDelete.Appearance.Options.UseForeColor = true;
        this.btnDelete.Location = new System.Drawing.Point(430, 10);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(100, 32);
        this.btnDelete.TabIndex = 2;
        this.btnDelete.Text = "🗑 Delete";
        
        // Save Button
        this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
        this.btnSave.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
        this.btnSave.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.btnSave.Appearance.Options.UseFont = true;
        this.btnSave.Appearance.Options.UseForeColor = true;
        this.btnSave.Location = new System.Drawing.Point(545, 10);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(100, 32);
        this.btnSave.TabIndex = 3;
        this.btnSave.Text = "💾 Save";
        
        // ════════════════════════════════════════════════════════════════════
        // NOTES CONTROL
        // ════════════════════════════════════════════════════════════════════
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitMain);
        this.Controls.Add(this.pnlHeader);
        this.Name = "NotesControl";
        this.Padding = new System.Windows.Forms.Padding(15);
        this.Size = new System.Drawing.Size(980, 580);
        
        ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
        this.pnlHeader.ResumeLayout(false);
        this.pnlHeader.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.txtSearch.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
        this.splitMain.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
        this.splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pnlNotesList)).EndInit();
        this.pnlNotesList.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.listNotes)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlEditor)).EndInit();
        this.pnlEditor.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.txtTitle.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.txtContent.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlEditorFooter)).EndInit();
        this.pnlEditorFooter.ResumeLayout(false);
        this.pnlEditorFooter.PerformLayout();
        this.ResumeLayout(false);
    }

    // Header
    private DevExpress.XtraEditors.PanelControl pnlHeader;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraEditors.SearchControl txtSearch;
    private DevExpress.XtraEditors.SimpleButton btnNewNote;
    
    // Main
    private DevExpress.XtraEditors.SplitContainerControl splitMain;
    
    // Notes List
    private DevExpress.XtraEditors.GroupControl pnlNotesList;
    private DevExpress.XtraEditors.ListBoxControl listNotes;
    
    // Editor
    private DevExpress.XtraEditors.GroupControl pnlEditor;
    private DevExpress.XtraEditors.TextEdit txtTitle;
    private DevExpress.XtraEditors.MemoEdit txtContent;
    private DevExpress.XtraEditors.PanelControl pnlEditorFooter;
    private DevExpress.XtraEditors.LabelControl lblWordCount;
    private DevExpress.XtraEditors.LabelControl lblLastSaved;
    private DevExpress.XtraEditors.SimpleButton btnSave;
    private DevExpress.XtraEditors.SimpleButton btnDelete;
    private System.Windows.Forms.FlowLayoutPanel pnlImages;
    private System.Windows.Forms.Label lblDropHint;
}
