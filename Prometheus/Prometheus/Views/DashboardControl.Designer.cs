namespace KeganOS.Views;

partial class DashboardControl
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
        this.components = new System.ComponentModel.Container();
        this.pnlMotivation = new DevExpress.XtraEditors.PanelControl();
        this.lblMotivation = new DevExpress.XtraEditors.LabelControl();
        this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
        this.pnlTimer = new DevExpress.XtraEditors.GroupControl();
        this.btnStart = new DevExpress.XtraEditors.SimpleButton();
        this.lblTimerDisplay = new DevExpress.XtraEditors.LabelControl();
        this.picTimerBrand = new DevExpress.XtraEditors.PictureEdit();
        this.tmrJourneyStream = new System.Windows.Forms.Timer(this.components);
        this.pnlStats = new DevExpress.XtraEditors.GroupControl();
        this.progressHeatmapLoading = new DevExpress.XtraWaitForm.ProgressPanel();
        this.progressXP = new DevExpress.XtraEditors.ProgressBarControl();
        this.lblLevelText = new DevExpress.XtraEditors.LabelControl();
        this.tileStats = new DevExpress.XtraEditors.TileControl();
        this.tileGroupStats = new DevExpress.XtraEditors.TileGroup();
        this.tileItemToday = new DevExpress.XtraEditors.TileItem();
        this.tileItemWeek = new DevExpress.XtraEditors.TileItem();
        this.tileItemTotal = new DevExpress.XtraEditors.TileItem();
        this.tileItemStreak = new DevExpress.XtraEditors.TileItem();
        this.picPixelaHeatmap = new DevExpress.XtraEditors.PictureEdit();
        this.btnManualTime = new DevExpress.XtraEditors.SimpleButton();
        this.btnShowJournal = new DevExpress.XtraEditors.SimpleButton();
        this.lblPixelaColor = new DevExpress.XtraEditors.LabelControl();
        this.cboPixelaColor = new DevExpress.XtraEditors.ComboBoxEdit();
        // StepProgressBar removed due to assembly compatibility issues
        // StepProgressBar removed due to assembly compatibility issues
        
        ((System.ComponentModel.ISupportInitialize)(this.pnlMotivation)).BeginInit();
        this.pnlMotivation.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
        this.splitMain.Panel1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
        this.splitMain.Panel2.SuspendLayout();
        this.splitMain.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pnlTimer)).BeginInit();
        this.pnlTimer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pnlStats)).BeginInit();
        this.pnlStats.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.picPixelaHeatmap.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.progressXP.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.picTimerBrand.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.cboPixelaColor.Properties)).BeginInit();
        // EndInit removed
        this.SuspendLayout();
        // 
        // pnlMotivation
        // 
        this.pnlMotivation.Controls.Add(this.lblMotivation);
        this.pnlMotivation.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlMotivation.Location = new System.Drawing.Point(15, 15);
        this.pnlMotivation.Name = "pnlMotivation";
        this.pnlMotivation.Size = new System.Drawing.Size(950, 50);
        this.pnlMotivation.TabIndex = 0;
        // 
        // lblMotivation
        // 
        this.lblMotivation.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic);
        this.lblMotivation.Appearance.Options.UseFont = true;
        this.lblMotivation.Appearance.Options.UseFont = true;
        this.lblMotivation.Dock = System.Windows.Forms.DockStyle.None;
        this.lblMotivation.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Default;
        this.lblMotivation.Location = new System.Drawing.Point(950, 10);
        this.lblMotivation.Name = "lblMotivation";
        this.lblMotivation.Padding = new System.Windows.Forms.Padding(10);
        this.lblMotivation.Size = new System.Drawing.Size(946, 46);
        this.lblMotivation.TabIndex = 0;
        this.lblMotivation.Text = "\"Small steps lead to big changes.\"";
        // 
        // splitMain
        // 
        this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
        this.splitMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
        this.splitMain.Location = new System.Drawing.Point(15, 65);
        this.splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        this.splitMain.Panel1.Controls.Add(this.pnlTimer);
        this.splitMain.Panel1.Text = "Panel1";
        // 
        // splitMain.Panel2
        // 
        this.splitMain.Panel2.Controls.Add(this.pnlStats);
        this.splitMain.Panel2.Text = "Panel2";
        this.splitMain.Size = new System.Drawing.Size(950, 500);
        this.splitMain.SplitterPosition = 450;
        this.splitMain.TabIndex = 1;
        // 
        // pnlTimer - KEGOMODORO
        // 
        this.pnlTimer.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlTimer.Location = new System.Drawing.Point(0, 0);
        this.pnlTimer.Name = "pnlTimer";
        this.pnlTimer.Size = new System.Drawing.Size(450, 500);
        this.pnlTimer.TabIndex = 0;
        this.pnlTimer.Text = "KEGOMODORO";
        this.pnlTimer.Controls.Add(this.btnStart);
        this.pnlTimer.Controls.Add(this.lblTimerDisplay);
        this.pnlTimer.Controls.Add(this.lblTimerDisplay);
        this.pnlTimer.Controls.Add(this.picTimerBrand);
        // Controls added
        // 
        // lblTimerDisplay
        // 
        this.lblTimerDisplay.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 72F);
        this.lblTimerDisplay.Appearance.Options.UseFont = true;
        this.lblTimerDisplay.Appearance.Options.UseTextOptions = true;
        this.lblTimerDisplay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        this.lblTimerDisplay.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
        this.lblTimerDisplay.Location = new System.Drawing.Point(50, 40);
        this.lblTimerDisplay.Name = "lblTimerDisplay";
        this.lblTimerDisplay.Size = new System.Drawing.Size(350, 120);
        this.lblTimerDisplay.TabIndex = 0;
        this.lblTimerDisplay.Text = "25:00";
        this.lblTimerDisplay.Visible = false;
        // 
        // picTimerBrand
        // 
        this.picTimerBrand.Location = new System.Drawing.Point(50, 40);
        this.picTimerBrand.Name = "picTimerBrand";
        this.picTimerBrand.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
        this.picTimerBrand.Properties.Appearance.Options.UseBackColor = true;
        this.picTimerBrand.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.picTimerBrand.Properties.NullText = " ";
        this.picTimerBrand.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
        this.picTimerBrand.Size = new System.Drawing.Size(350, 160);
        this.picTimerBrand.TabIndex = 4;
        //
        // Items added
        // 
        // tmrJourneyStream
        // 
        this.tmrJourneyStream.Interval = 50;
        // 
        // btnStart
        // 
        this.btnStart.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.btnStart.Appearance.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
        this.btnStart.Appearance.Options.UseFont = true;
        // 
        // btnStart
        // 
        this.btnStart.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.btnStart.Appearance.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
        this.btnStart.Appearance.Options.UseFont = true;
        this.btnStart.Appearance.Options.UseForeColor = true;
        this.btnStart.Location = new System.Drawing.Point(170, 200); // Moved Up
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(110, 35);
        this.btnStart.TabIndex = 1;
        this.btnStart.Text = "[ ▶ Start ]";
        
        // 
        // grpTasks
        // 
        // pageDaily - instantiated first
        this.pageDaily = new DevExpress.XtraTab.XtraTabPage();
        this.pageDaily.Text = "Daily";
        this.lstDaily = new DevExpress.XtraEditors.CheckedListBoxControl();
        this.lstDaily.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lstDaily.SelectionMode = System.Windows.Forms.SelectionMode.One;
        this.pageDaily.Controls.Add(this.lstDaily);

        // pageLongTerm
        this.pageLongTerm = new DevExpress.XtraTab.XtraTabPage();
        this.pageLongTerm.Text = "Long Term";
        this.lstLongTerm = new DevExpress.XtraEditors.CheckedListBoxControl();
        this.lstLongTerm.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pageLongTerm.Controls.Add(this.lstLongTerm);

        // pageDone
        this.pageDone = new DevExpress.XtraTab.XtraTabPage();
        this.pageDone.Text = "Done";
        this.lstDone = new DevExpress.XtraEditors.CheckedListBoxControl();
        this.lstDone.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pageDone.Controls.Add(this.lstDone);

        // tabTasks - now added
        this.tabTasks = new DevExpress.XtraTab.XtraTabControl();
        this.tabTasks.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tabTasks.TabPages.Add(this.pageDaily);
        this.tabTasks.TabPages.Add(this.pageLongTerm);
        this.tabTasks.TabPages.Add(this.pageDone);

        // btnDeleteTask
        this.btnDeleteTask = new DevExpress.XtraEditors.SimpleButton();
        this.btnDeleteTask.Text = "X";
        this.btnDeleteTask.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnDeleteTask.Width = 30;
        this.btnDeleteTask.ToolTip = "Delete Selected";

        // btnAddTask
        this.btnAddTask = new DevExpress.XtraEditors.SimpleButton();
        this.btnAddTask.Text = "+";
        this.btnAddTask.Dock = System.Windows.Forms.DockStyle.Right;
        this.btnAddTask.Width = 30;
        this.btnAddTask.ToolTip = "Add Task";

        // txtNewTask (Action Panel version)
        this.txtNewTask = new DevExpress.XtraEditors.TextEdit();
        this.txtNewTask.Dock = System.Windows.Forms.DockStyle.Fill;
        this.txtNewTask.Properties.NullText = "+ Add Task";
        this.txtNewTask.Properties.Appearance.BackColor = System.Drawing.Color.WhiteSmoke;
        this.txtNewTask.Properties.Appearance.Options.UseBackColor = true;

        // pnlTaskActions
        this.pnlTaskActions = new DevExpress.XtraEditors.PanelControl();
        this.pnlTaskActions.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.pnlTaskActions.Height = 30;
        this.pnlTaskActions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlTaskActions.Controls.Add(this.txtNewTask);
        this.pnlTaskActions.Controls.Add(this.btnAddTask);
        this.pnlTaskActions.Controls.Add(this.btnDeleteTask);
        
        // grpTasks
        this.grpTasks = new DevExpress.XtraEditors.GroupControl();
        this.grpTasks.Text = "Short Task Manager";
        this.grpTasks.Location = new System.Drawing.Point(25, 260); 
        this.grpTasks.Size = new System.Drawing.Size(400, 230); 
        this.grpTasks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
        this.grpTasks.Controls.Add(this.tabTasks); // Tab control is ready
        this.grpTasks.Controls.Add(this.pnlTaskActions); // Actions Panel is ready
        this.pnlTimer.Controls.Add(this.grpTasks);
        
        // Flush button will be added to Daily tab header dynamically or context menu
        // 
        // pnlStats - Your Stats
        // 
        this.pnlStats.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlStats.Location = new System.Drawing.Point(0, 0);
        this.pnlStats.Name = "pnlStats";
        this.pnlStats.Size = new System.Drawing.Size(490, 500);
        this.pnlStats.TabIndex = 0;
        this.pnlStats.Controls.Add(this.tileStats);
        this.pnlStats.Controls.Add(this.progressHeatmapLoading);
        this.pnlStats.Controls.Add(this.picPixelaHeatmap);
        this.pnlStats.Controls.Add(this.progressXP);
        this.pnlStats.Controls.Add(this.lblLevelText);
        this.pnlStats.Controls.Add(this.lblPixelaColor);
        this.pnlStats.Controls.Add(this.cboPixelaColor);
        // 
        // tileStats
        // 
        this.tileStats.Groups.Add(this.tileGroupStats);
        this.tileGroupStats.Items.Add(this.tileItemToday);
        this.tileGroupStats.Items.Add(this.tileItemWeek);
        this.tileGroupStats.Items.Add(this.tileItemTotal);
        this.tileGroupStats.Items.Add(this.tileItemStreak);
        this.tileStats.Location = new System.Drawing.Point(20, 70); 
        this.tileStats.Name = "tileStats";
        this.tileStats.Size = new System.Drawing.Size(450, 210); 
        this.tileStats.TabIndex = 23;
        this.tileStats.ColumnCount = 2;
        this.tileStats.Orientation = System.Windows.Forms.Orientation.Vertical;
        this.tileStats.ItemSize = 100;
        this.tileStats.IndentBetweenItems = 10;
        
        // tileItemToday
        this.tileItemToday.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(0, 120, 212);
        this.tileItemToday.Name = "tileItemToday";
        this.tileItemToday.Text = "Today";
        
        // tileItemWeek
        this.tileItemWeek.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
        this.tileItemWeek.Name = "tileItemWeek";
        this.tileItemWeek.Text = "Week";

        // btnManualTime and btnShowJournal relocated to header next to each other
        this.pnlStats.Controls.Add(this.btnManualTime);
        this.pnlStats.Controls.Add(this.btnShowJournal);
        
        // tileItemTotal
        this.tileItemTotal.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
        this.tileItemTotal.Name = "tileItemTotal";
        this.tileItemTotal.Text = "Total";
        
        // tileItemStreak
        this.tileItemStreak.AppearanceItem.Normal.BackColor = System.Drawing.Color.FromArgb(255, 152, 0);
        this.tileItemStreak.Name = "tileItemStreak";
        this.tileItemStreak.Text = "Streak";
        // 
        // progressHeatmapLoading
        // 
        this.progressHeatmapLoading.Appearance.BackColor = System.Drawing.Color.Transparent;
        this.progressHeatmapLoading.Appearance.Options.UseBackColor = true;
        this.progressHeatmapLoading.BarAnimationElementThickness = 3;
        this.progressHeatmapLoading.Location = new System.Drawing.Point(195, 370);
        this.progressHeatmapLoading.Name = "progressHeatmapLoading";
        this.progressHeatmapLoading.ShowCaption = false;
        this.progressHeatmapLoading.ShowDescription = false;
        this.progressHeatmapLoading.Size = new System.Drawing.Size(100, 40);
        this.progressHeatmapLoading.TabIndex = 22;
        this.progressHeatmapLoading.Visible = false;
        // 
        // btnManualTime
        // 
        this.btnManualTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnManualTime.Appearance.Options.UseFont = true;
        this.btnManualTime.Location = new System.Drawing.Point(300, 35); 
        this.btnManualTime.Name = "btnManualTime";
        this.btnManualTime.Size = new System.Drawing.Size(80, 24);
        this.btnManualTime.TabIndex = 20;
        this.btnManualTime.Text = "+ Log Time";
        this.btnManualTime.ToolTip = "Manually add hours to Pixe.la";
        // 
        // btnShowJournal
        // 
        this.btnShowJournal.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnShowJournal.Appearance.Options.UseFont = true;
        this.btnShowJournal.Location = new System.Drawing.Point(390, 35); 
        this.btnShowJournal.Name = "btnShowJournal";
        this.btnShowJournal.Size = new System.Drawing.Size(80, 24);
        this.btnShowJournal.TabIndex = 21;
        this.btnShowJournal.Text = "Notebook";
        this.btnShowJournal.ToolTip = "Open Daily Journal";
        
        //
        // lblPixelaColor
        //
        this.lblPixelaColor.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F);
        this.lblPixelaColor.Appearance.ForeColor = System.Drawing.Color.DimGray;
        this.lblPixelaColor.Location = new System.Drawing.Point(300, 480);
        this.lblPixelaColor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.lblPixelaColor.Name = "lblPixelaColor";
        this.lblPixelaColor.Size = new System.Drawing.Size(100, 15);
        this.lblPixelaColor.TabIndex = 24;
        this.lblPixelaColor.Text = "Theme";
        //
        // cboPixelaColor
        //
        this.cboPixelaColor.Location = new System.Drawing.Point(340, 477);
        this.cboPixelaColor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
        this.cboPixelaColor.Name = "cboPixelaColor";
        this.cboPixelaColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        this.cboPixelaColor.Properties.Items.AddRange(new object[] {
            "shibafu (Green)",
            "momiji (Red)",
            "sora (Blue)",
            "ichou (Yellow)",
            "ajisai (Purple)",
            "kuro (Black)"});
        this.cboPixelaColor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
        this.cboPixelaColor.Size = new System.Drawing.Size(130, 20);
        this.cboPixelaColor.TabIndex = 25;
        // 
        // lblLevelText
        // 
        this.lblLevelText.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblLevelText.Appearance.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
        this.lblLevelText.Appearance.Options.UseFont = true;
        this.lblLevelText.Appearance.Options.UseForeColor = true;
        this.lblLevelText.Location = new System.Drawing.Point(30, 160);
        this.lblLevelText.Name = "lblLevelText";
        this.lblLevelText.Size = new System.Drawing.Size(150, 20);
        this.lblLevelText.TabIndex = 6;
        this.lblLevelText.Text = "Level 1 • 0/100 XP";
        // 
        // progressXP
        // 
        this.progressXP.Location = new System.Drawing.Point(30, 190);
        this.progressXP.Name = "progressXP";
        this.progressXP.Properties.ShowTitle = false;
        this.progressXP.Size = new System.Drawing.Size(400, 20);
        this.progressXP.TabIndex = 7;
        // 
        // picPixelaHeatmap
        // 
        this.picPixelaHeatmap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
        this.picPixelaHeatmap.Location = new System.Drawing.Point(20, 300);
        this.picPixelaHeatmap.Name = "picPixelaHeatmap";
        this.picPixelaHeatmap.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never;
        this.picPixelaHeatmap.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
        this.picPixelaHeatmap.Size = new System.Drawing.Size(450, 180);
        this.picPixelaHeatmap.TabIndex = 12;
        this.picPixelaHeatmap.ToolTip = "Pixe.la Heatmap";
        // 
        // DashboardControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.splitMain);
        this.Controls.Add(this.pnlMotivation);
        this.Name = "DashboardControl";
        this.Padding = new System.Windows.Forms.Padding(15);
        this.Size = new System.Drawing.Size(980, 620);
        ((System.ComponentModel.ISupportInitialize)(this.pnlMotivation)).EndInit();
        this.pnlMotivation.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
        this.splitMain.Panel1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
        this.splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
        this.splitMain.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pnlTimer)).EndInit();
        this.pnlTimer.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pnlStats)).EndInit();
        this.pnlStats.ResumeLayout(false);
        this.pnlStats.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.picPixelaHeatmap.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.picTimerBrand.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.progressXP.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.progressXP.Properties)).EndInit();
        this.ResumeLayout(false);
    }

    private DevExpress.XtraEditors.PanelControl pnlMotivation;
    private DevExpress.XtraEditors.LabelControl lblMotivation;
    private DevExpress.XtraEditors.SplitContainerControl splitMain;
    private DevExpress.XtraEditors.GroupControl pnlTimer;
    private DevExpress.XtraEditors.LabelControl lblTimerDisplay;
    private DevExpress.XtraEditors.SimpleButton btnStart;
    private DevExpress.XtraEditors.SimpleButton btnManualTime;
    private DevExpress.XtraEditors.SimpleButton btnShowJournal;
    private DevExpress.XtraEditors.GroupControl pnlStats;
    private DevExpress.XtraWaitForm.ProgressPanel progressHeatmapLoading;
    private DevExpress.XtraEditors.TileControl tileStats;
    private DevExpress.XtraEditors.TileGroup tileGroupStats;
    private DevExpress.XtraEditors.TileItem tileItemToday;
    private DevExpress.XtraEditors.TileItem tileItemWeek;
    private DevExpress.XtraEditors.TileItem tileItemTotal;
    private DevExpress.XtraEditors.TileItem tileItemStreak;
    private DevExpress.XtraEditors.PictureEdit picTimerBrand;
    private DevExpress.XtraEditors.PictureEdit picPixelaHeatmap;
    private DevExpress.XtraEditors.ProgressBarControl progressXP;
    private DevExpress.XtraEditors.LabelControl lblLevelText;
    private System.Windows.Forms.Timer tmrJourneyStream;
    // New Controls
    // stepDailyProgress field removed
    private DevExpress.XtraEditors.LabelControl lblPixelaColor;
    private DevExpress.XtraEditors.ComboBoxEdit cboPixelaColor;
    // Task Manager Controls
    private DevExpress.XtraEditors.GroupControl grpTasks;
    private DevExpress.XtraTab.XtraTabControl tabTasks;
    private DevExpress.XtraTab.XtraTabPage pageDaily;
    private DevExpress.XtraTab.XtraTabPage pageLongTerm;
    private DevExpress.XtraTab.XtraTabPage pageDone;
    private DevExpress.XtraEditors.CheckedListBoxControl lstDaily;
    private DevExpress.XtraEditors.CheckedListBoxControl lstLongTerm;
    private DevExpress.XtraEditors.CheckedListBoxControl lstDone;
    private DevExpress.XtraEditors.TextEdit txtNewTask;
    private DevExpress.XtraEditors.SimpleButton btnAddTask;
    private DevExpress.XtraEditors.SimpleButton btnDeleteTask;
    private DevExpress.XtraEditors.PanelControl pnlTaskActions;
    private DevExpress.XtraEditors.SimpleButton btnFlush;
}
