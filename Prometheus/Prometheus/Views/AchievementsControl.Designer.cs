namespace KeganOS.Views;

partial class AchievementsControl
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
        this.tileControlAchievements = new DevExpress.XtraEditors.TileControl();
        this.tileGroupMain = new DevExpress.XtraEditors.TileGroup();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        this.SuspendLayout();
        // 
        // tileControlAchievements
        // 
        this.tileControlAchievements.AllowDrag = false;
        this.tileControlAchievements.AppearanceItem.Normal.BackColor = System.Drawing.Color.White;
        this.tileControlAchievements.AppearanceItem.Normal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
        this.tileControlAchievements.AppearanceItem.Normal.Options.UseBackColor = true;
        this.tileControlAchievements.AppearanceItem.Normal.Options.UseBorderColor = true;
        this.tileControlAchievements.Dock = System.Windows.Forms.DockStyle.Fill;
        this.tileControlAchievements.Groups.Add(this.tileGroupMain);
        this.tileControlAchievements.ItemSize = 150;
        this.tileControlAchievements.Location = new System.Drawing.Point(20, 70);
        this.tileControlAchievements.MaxId = 1;
        this.tileControlAchievements.Name = "tileControlAchievements";
        this.tileControlAchievements.Padding = new System.Windows.Forms.Padding(10);
        this.tileControlAchievements.Size = new System.Drawing.Size(560, 360);
        this.tileControlAchievements.TabIndex = 0;
        this.tileControlAchievements.Text = "Achievements";
        // 
        // tileGroupMain
        // 
        this.tileGroupMain.Name = "tileGroupMain";
        this.tileGroupMain.Text = "Your Journey";
        // 
        // lblTitle
        // 
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 20F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(20, 20, 20);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Appearance.Options.UseForeColor = true;
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Location = new System.Drawing.Point(20, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new System.Windows.Forms.Padding(10, 5, 0, 15);
        this.lblTitle.Size = new System.Drawing.Size(250, 60);
        this.lblTitle.TabIndex = 1;
        this.lblTitle.Text = "Hall of Fame";
        this.lblTitle.BackColor = System.Drawing.Color.Transparent;
        // 
        // AchievementsControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
        this.Controls.Add(this.tileControlAchievements);
        this.Controls.Add(this.lblTitle);
        this.Name = "AchievementsControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraEditors.TileControl tileControlAchievements;
    private DevExpress.XtraEditors.TileGroup tileGroupMain;
    private DevExpress.XtraEditors.LabelControl lblTitle;
}
