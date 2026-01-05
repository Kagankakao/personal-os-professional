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
        this.pnlMotivation = new DevExpress.XtraEditors.PanelControl();
        this.lblMotivation = new DevExpress.XtraEditors.LabelControl();
        this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
        this.pnlTimer = new DevExpress.XtraEditors.GroupControl();
        this.btnLaunchKegomoDoro = new DevExpress.XtraEditors.SimpleButton();
        this.btnCustomize = new DevExpress.XtraEditors.SimpleButton();
        this.btnStart = new DevExpress.XtraEditors.SimpleButton();
        this.lblTimerDisplay = new DevExpress.XtraEditors.LabelControl();
        this.pnlStats = new DevExpress.XtraEditors.GroupControl();
        this.progressHeatmapLoading = new DevExpress.XtraWaitForm.ProgressPanel();
        this.picPixelaHeatmap = new DevExpress.XtraEditors.PictureEdit();
        this.lblBestValue = new DevExpress.XtraEditors.LabelControl();
        this.lblBest = new DevExpress.XtraEditors.LabelControl();
        this.btnManualTime = new DevExpress.XtraEditors.SimpleButton();
        this.btnShowJournal = new DevExpress.XtraEditors.SimpleButton();
        this.lblStreakValue = new DevExpress.XtraEditors.LabelControl();
        this.lblStreak = new DevExpress.XtraEditors.LabelControl();
        this.progressXP = new DevExpress.XtraEditors.ProgressBarControl();
        this.lblLevelText = new DevExpress.XtraEditors.LabelControl();
        this.lblTotalValue = new DevExpress.XtraEditors.LabelControl();
        this.lblTotal = new DevExpress.XtraEditors.LabelControl();
        this.lblWeekValue = new DevExpress.XtraEditors.LabelControl();
        this.lblWeek = new DevExpress.XtraEditors.LabelControl();
        this.lblTodayValue = new DevExpress.XtraEditors.LabelControl();
        this.lblToday = new DevExpress.XtraEditors.LabelControl();
        
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
        this.lblMotivation.Dock = System.Windows.Forms.DockStyle.Fill;
        this.lblMotivation.Location = new System.Drawing.Point(2, 2);
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
        this.pnlTimer.Controls.Add(this.btnLaunchKegomoDoro);
        this.pnlTimer.Controls.Add(this.btnCustomize);
        this.pnlTimer.Controls.Add(this.btnStart);
        this.pnlTimer.Controls.Add(this.lblTimerDisplay);
        // 
        // lblTimerDisplay
        // 
        this.lblTimerDisplay.Appearance.Font = new System.Drawing.Font("Segoe UI Light", 72F);
        this.lblTimerDisplay.Appearance.Options.UseFont = true;
        this.lblTimerDisplay.Appearance.Options.UseTextOptions = true;
        this.lblTimerDisplay.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        this.lblTimerDisplay.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
        this.lblTimerDisplay.Location = new System.Drawing.Point(50, 80);
        this.lblTimerDisplay.Name = "lblTimerDisplay";
        this.lblTimerDisplay.Size = new System.Drawing.Size(350, 120);
        this.lblTimerDisplay.TabIndex = 0;
        this.lblTimerDisplay.Text = "25:00";
        // 
        // btnStart
        // 
        this.btnStart.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.btnStart.Appearance.ForeColor = System.Drawing.Color.FromArgb(76, 175, 80);
        this.btnStart.Appearance.Options.UseFont = true;
        this.btnStart.Appearance.Options.UseForeColor = true;
        this.btnStart.Location = new System.Drawing.Point(100, 220);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(100, 35);
        this.btnStart.TabIndex = 1;
        this.btnStart.Text = "[ ▶ Start ]";
        // 
        // btnCustomize
        // 
        this.btnCustomize.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.btnCustomize.Appearance.Options.UseFont = true;
        this.btnCustomize.Location = new System.Drawing.Point(220, 220);
        this.btnCustomize.Name = "btnCustomize";
        this.btnCustomize.Size = new System.Drawing.Size(130, 35);
        this.btnCustomize.TabIndex = 2;
        this.btnCustomize.Text = "[ ⚙Customize ]";
        // 
        // btnLaunchKegomoDoro
        // 
        this.btnLaunchKegomoDoro.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
        this.btnLaunchKegomoDoro.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.btnLaunchKegomoDoro.Appearance.Options.UseFont = true;
        this.btnLaunchKegomoDoro.Appearance.Options.UseForeColor = true;
        this.btnLaunchKegomoDoro.Location = new System.Drawing.Point(100, 420);
        this.btnLaunchKegomoDoro.Name = "btnLaunchKegomoDoro";
        this.btnLaunchKegomoDoro.Size = new System.Drawing.Size(250, 35);
        this.btnLaunchKegomoDoro.TabIndex = 3;
        this.btnLaunchKegomoDoro.Text = "[ Launch Full KegomoDoro ]";
        this.btnLaunchKegomoDoro.Click += new System.EventHandler(this.BtnLaunchKegomoDoro_Click);
        // 
        // pnlStats - Your Stats
        // 
        this.pnlStats.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pnlStats.Location = new System.Drawing.Point(0, 0);
        this.pnlStats.Name = "pnlStats";
        this.pnlStats.Size = new System.Drawing.Size(490, 500);
        this.pnlStats.TabIndex = 0;
        this.pnlStats.Text = "📊 Your Stats";
        this.pnlStats.Controls.Add(this.progressHeatmapLoading);
        this.pnlStats.Controls.Add(this.picPixelaHeatmap);
        this.pnlStats.Controls.Add(this.btnManualTime);
        this.pnlStats.Controls.Add(this.btnShowJournal);
        this.pnlStats.Controls.Add(this.lblBestValue);
        this.pnlStats.Controls.Add(this.lblBest);
        this.pnlStats.Controls.Add(this.lblStreakValue);
        this.pnlStats.Controls.Add(this.lblStreak);
        this.pnlStats.Controls.Add(this.progressXP);
        this.pnlStats.Controls.Add(this.lblLevelText);
        this.pnlStats.Controls.Add(this.lblTotalValue);
        this.pnlStats.Controls.Add(this.lblTotal);
        this.pnlStats.Controls.Add(this.lblWeekValue);
        this.pnlStats.Controls.Add(this.lblWeek);
        this.pnlStats.Controls.Add(this.lblTodayValue);
        this.pnlStats.Controls.Add(this.lblToday);
        // 
        // progressHeatmapLoading
        // 
        this.progressHeatmapLoading.Appearance.BackColor = System.Drawing.Color.Transparent;
        this.progressHeatmapLoading.Appearance.Options.UseBackColor = true;
        this.progressHeatmapLoading.BarAnimationElementThickness = 2;
        this.progressHeatmapLoading.Caption = "Refreshing Heatmap...";
        this.progressHeatmapLoading.Description = "Please wait";
        this.progressHeatmapLoading.Location = new System.Drawing.Point(145, 360);
        this.progressHeatmapLoading.Name = "progressHeatmapLoading";
        this.progressHeatmapLoading.Size = new System.Drawing.Size(200, 60);
        this.progressHeatmapLoading.TabIndex = 22;
        this.progressHeatmapLoading.Text = "progressPanel1";
        this.progressHeatmapLoading.Visible = false;
        // 
        // btnManualTime
        // 
        this.btnManualTime.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnManualTime.Appearance.Options.UseFont = true;
        this.btnManualTime.Location = new System.Drawing.Point(300, 78);
        this.btnManualTime.Name = "btnManualTime";
        this.btnManualTime.Size = new System.Drawing.Size(80, 24);
        this.btnManualTime.TabIndex = 20;
        this.btnManualTime.Text = "+ Log Time";
        this.btnManualTime.TabIndex = 20;
        this.btnManualTime.Text = "+ Log Time";
        this.btnManualTime.ToolTip = "Manually add hours to Pixe.la";
        // 
        // btnShowJournal
        // 
        this.btnShowJournal.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
        this.btnShowJournal.Appearance.Options.UseFont = true;
        this.btnShowJournal.Location = new System.Drawing.Point(300, 108);
        this.btnShowJournal.Name = "btnShowJournal";
        this.btnShowJournal.Size = new System.Drawing.Size(80, 24);
        this.btnShowJournal.TabIndex = 21;
        this.btnShowJournal.Text = "Notebook";
        this.btnShowJournal.ToolTip = "Open Daily Journal";
        // 
        // lblToday
        // 
        this.lblToday.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblToday.Location = new System.Drawing.Point(30, 50);
        this.lblToday.Name = "lblToday";
        this.lblToday.Size = new System.Drawing.Size(50, 20);
        this.lblToday.TabIndex = 0;
        this.lblToday.Text = "Today:";
        // 
        // lblTodayValue
        // 
        this.lblTodayValue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblTodayValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.lblTodayValue.Appearance.Options.UseFont = true;
        this.lblTodayValue.Appearance.Options.UseForeColor = true;
        this.lblTodayValue.Location = new System.Drawing.Point(200, 50);
        this.lblTodayValue.Name = "lblTodayValue";
        this.lblTodayValue.Size = new System.Drawing.Size(50, 20);
        this.lblTodayValue.TabIndex = 1;
        this.lblTodayValue.Text = "0 hrs";
        // 
        // lblWeek
        // 
        this.lblWeek.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblWeek.Location = new System.Drawing.Point(30, 80);
        this.lblWeek.Name = "lblWeek";
        this.lblWeek.Size = new System.Drawing.Size(50, 20);
        this.lblWeek.TabIndex = 2;
        this.lblWeek.Text = "Week:";
        // 
        // lblWeekValue
        // 
        this.lblWeekValue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblWeekValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.lblWeekValue.Appearance.Options.UseFont = true;
        this.lblWeekValue.Appearance.Options.UseForeColor = true;
        this.lblWeekValue.Location = new System.Drawing.Point(200, 80);
        this.lblWeekValue.Name = "lblWeekValue";
        this.lblWeekValue.Size = new System.Drawing.Size(50, 20);
        this.lblWeekValue.TabIndex = 3;
        this.lblWeekValue.Text = "0 hrs";
        // 
        // lblTotal
        // 
        this.lblTotal.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblTotal.Location = new System.Drawing.Point(30, 110);
        this.lblTotal.Name = "lblTotal";
        this.lblTotal.Size = new System.Drawing.Size(50, 20);
        this.lblTotal.TabIndex = 4;
        this.lblTotal.Text = "Total:";
        // 
        // lblTotalValue
        // 
        this.lblTotalValue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblTotalValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.lblTotalValue.Appearance.Options.UseFont = true;
        this.lblTotalValue.Appearance.Options.UseForeColor = true;
        this.lblTotalValue.Location = new System.Drawing.Point(200, 110);
        this.lblTotalValue.Name = "lblTotalValue";
        this.lblTotalValue.Size = new System.Drawing.Size(60, 20);
        this.lblTotalValue.TabIndex = 5;
        this.lblTotalValue.Text = "0 hrs";
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
        // lblStreak
        // 
        this.lblStreak.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblStreak.Location = new System.Drawing.Point(30, 240);
        this.lblStreak.Name = "lblStreak";
        this.lblStreak.Size = new System.Drawing.Size(70, 20);
        this.lblStreak.TabIndex = 8;
        this.lblStreak.Text = "◎ Streak:";
        // 
        // lblStreakValue
        // 
        this.lblStreakValue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblStreakValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.lblStreakValue.Appearance.Options.UseFont = true;
        this.lblStreakValue.Appearance.Options.UseForeColor = true;
        this.lblStreakValue.Location = new System.Drawing.Point(200, 240);
        this.lblStreakValue.Name = "lblStreakValue";
        this.lblStreakValue.Size = new System.Drawing.Size(60, 20);
        this.lblStreakValue.TabIndex = 9;
        this.lblStreakValue.Text = "0 days";
        // 
        // lblBest
        // 
        this.lblBest.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
        this.lblBest.Location = new System.Drawing.Point(30, 270);
        this.lblBest.Name = "lblBest";
        this.lblBest.Size = new System.Drawing.Size(60, 20);
        this.lblBest.TabIndex = 10;
        this.lblBest.Text = "✿ Best:";
        // 
        // lblBestValue
        // 
        this.lblBestValue.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
        this.lblBestValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(0, 188, 212);
        this.lblBestValue.Appearance.Options.UseFont = true;
        this.lblBestValue.Appearance.Options.UseForeColor = true;
        this.lblBestValue.Location = new System.Drawing.Point(200, 270);
        this.lblBestValue.Name = "lblBestValue";
        this.lblBestValue.Size = new System.Drawing.Size(60, 20);
        this.lblBestValue.TabIndex = 11;
        this.lblBestValue.Text = "0 days";
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
        ((System.ComponentModel.ISupportInitialize)(this.progressXP.Properties)).EndInit();
        this.ResumeLayout(false);
    }

    private DevExpress.XtraEditors.PanelControl pnlMotivation;
    private DevExpress.XtraEditors.LabelControl lblMotivation;
    private DevExpress.XtraEditors.SplitContainerControl splitMain;
    private DevExpress.XtraEditors.GroupControl pnlTimer;
    private DevExpress.XtraEditors.LabelControl lblTimerDisplay;
    private DevExpress.XtraEditors.SimpleButton btnStart;
    private DevExpress.XtraEditors.SimpleButton btnCustomize;
    private DevExpress.XtraEditors.SimpleButton btnLaunchKegomoDoro;
    private DevExpress.XtraEditors.SimpleButton btnManualTime;
    private DevExpress.XtraEditors.SimpleButton btnShowJournal;
    private DevExpress.XtraEditors.GroupControl pnlStats;
    private DevExpress.XtraWaitForm.ProgressPanel progressHeatmapLoading;
    private DevExpress.XtraEditors.PictureEdit picPixelaHeatmap;
    private DevExpress.XtraEditors.LabelControl lblToday;
    private DevExpress.XtraEditors.LabelControl lblTodayValue;
    private DevExpress.XtraEditors.LabelControl lblWeek;
    private DevExpress.XtraEditors.LabelControl lblWeekValue;
    private DevExpress.XtraEditors.LabelControl lblTotal;
    private DevExpress.XtraEditors.LabelControl lblTotalValue;
    private DevExpress.XtraEditors.LabelControl lblLevelText;
    private DevExpress.XtraEditors.ProgressBarControl progressXP;
    private DevExpress.XtraEditors.LabelControl lblStreak;
    private DevExpress.XtraEditors.LabelControl lblStreakValue;
    private DevExpress.XtraEditors.LabelControl lblBest;
    private DevExpress.XtraEditors.LabelControl lblBestValue;
}
