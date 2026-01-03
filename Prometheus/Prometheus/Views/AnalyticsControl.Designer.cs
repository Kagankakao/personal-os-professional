namespace KeganOS.Views;

partial class AnalyticsControl
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
        this.chartWeeklyProgress = new DevExpress.XtraCharts.ChartControl();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        this.pnlSummary = new DevExpress.XtraEditors.PanelControl();
        this.lblTotalHours = new DevExpress.XtraEditors.LabelControl();
        this.lblTotalSessions = new DevExpress.XtraEditors.LabelControl();
        this.lblAverageSession = new DevExpress.XtraEditors.LabelControl();
        ((System.ComponentModel.ISupportInitialize)(this.chartWeeklyProgress)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlSummary)).BeginInit();
        this.pnlSummary.SuspendLayout();
        this.SuspendLayout();
        // 
        // chartWeeklyProgress
        // 
        this.chartWeeklyProgress.Dock = System.Windows.Forms.DockStyle.Fill;
        this.chartWeeklyProgress.Legend.Name = "Default Legend";
        this.chartWeeklyProgress.Location = new System.Drawing.Point(20, 150);
        this.chartWeeklyProgress.Name = "chartWeeklyProgress";
        this.chartWeeklyProgress.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
        this.chartWeeklyProgress.Size = new System.Drawing.Size(560, 280);
        this.chartWeeklyProgress.TabIndex = 0;
        // 
        // lblTitle
        // 
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Location = new System.Drawing.Point(20, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
        this.lblTitle.Size = new System.Drawing.Size(207, 42);
        this.lblTitle.TabIndex = 1;
        this.lblTitle.Text = "Productivity Radar";
        // 
        // pnlSummary
        // 
        this.pnlSummary.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
        this.pnlSummary.Controls.Add(this.lblAverageSession);
        this.pnlSummary.Controls.Add(this.lblTotalSessions);
        this.pnlSummary.Controls.Add(this.lblTotalHours);
        this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Top;
        this.pnlSummary.Location = new System.Drawing.Point(20, 62);
        this.pnlSummary.Name = "pnlSummary";
        this.pnlSummary.Size = new System.Drawing.Size(560, 88);
        this.pnlSummary.TabIndex = 2;
        // 
        // lblTotalHours
        // 
        this.lblTotalHours.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.lblTotalHours.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
        this.lblTotalHours.Appearance.Options.UseFont = true;
        this.lblTotalHours.Appearance.Options.UseForeColor = true;
        this.lblTotalHours.Location = new System.Drawing.Point(10, 10);
        this.lblTotalHours.Name = "lblTotalHours";
        this.lblTotalHours.Size = new System.Drawing.Size(147, 21);
        this.lblTotalHours.TabIndex = 0;
        this.lblTotalHours.Text = "Total Focus: -- hours";
        // 
        // lblTotalSessions
        // 
        this.lblTotalSessions.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.lblTotalSessions.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
        this.lblTotalSessions.Appearance.Options.UseFont = true;
        this.lblTotalSessions.Appearance.Options.UseForeColor = true;
        this.lblTotalSessions.Location = new System.Drawing.Point(10, 35);
        this.lblTotalSessions.Name = "lblTotalSessions";
        this.lblTotalSessions.Size = new System.Drawing.Size(125, 21);
        this.lblTotalSessions.TabIndex = 1;
        this.lblTotalSessions.Text = "Total Sessions: --";
        // 
        // lblAverageSession
        // 
        this.lblAverageSession.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
        this.lblAverageSession.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
        this.lblAverageSession.Appearance.Options.UseFont = true;
        this.lblAverageSession.Appearance.Options.UseForeColor = true;
        this.lblAverageSession.Location = new System.Drawing.Point(10, 60);
        this.lblAverageSession.Name = "lblAverageSession";
        this.lblAverageSession.Size = new System.Drawing.Size(171, 21);
        this.lblAverageSession.TabIndex = 2;
        this.lblAverageSession.Text = "Average Session: -- min";
        // 
        // AnalyticsControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.chartWeeklyProgress);
        this.Controls.Add(this.pnlSummary);
        this.Controls.Add(this.lblTitle);
        this.Name = "AnalyticsControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        ((System.ComponentModel.ISupportInitialize)(this.chartWeeklyProgress)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pnlSummary)).EndInit();
        this.pnlSummary.ResumeLayout(false);
        this.pnlSummary.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraCharts.ChartControl chartWeeklyProgress;
    private DevExpress.XtraEditors.LabelControl lblTitle;
    private DevExpress.XtraEditors.PanelControl pnlSummary;
    private DevExpress.XtraEditors.LabelControl lblTotalHours;
    private DevExpress.XtraEditors.LabelControl lblTotalSessions;
    private DevExpress.XtraEditors.LabelControl lblAverageSession;
}
