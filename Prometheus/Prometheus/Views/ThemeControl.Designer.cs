namespace KeganOS.Views;

partial class ThemeControl
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
        this.galleryControlThemes = new DevExpress.XtraBars.Ribbon.GalleryControl();
        this.galleryControlClient1 = new DevExpress.XtraBars.Ribbon.GalleryControlClient();
        this.lblTitle = new DevExpress.XtraEditors.LabelControl();
        ((System.ComponentModel.ISupportInitialize)(this.galleryControlThemes)).BeginInit();
        this.SuspendLayout();
        // 
        // galleryControlThemes
        // 
        this.galleryControlThemes.Controls.Add(this.galleryControlClient1);
        this.galleryControlThemes.Dock = System.Windows.Forms.DockStyle.Fill;
        // 
        // 
        // 
        this.galleryControlThemes.Gallery.Appearance.ItemCaptionAppearance.Normal.Options.UseTextOptions = true;
        this.galleryControlThemes.Gallery.Appearance.ItemCaptionAppearance.Normal.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        this.galleryControlThemes.Gallery.ImageSize = new System.Drawing.Size(120, 80);
        this.galleryControlThemes.Gallery.ItemCheckMode = DevExpress.XtraBars.Ribbon.Gallery.ItemCheckMode.SingleCheck;
        this.galleryControlThemes.Gallery.ShowItemText = true;
        this.galleryControlThemes.Location = new System.Drawing.Point(20, 70);
        this.galleryControlThemes.Name = "galleryControlThemes";
        this.galleryControlThemes.Size = new System.Drawing.Size(560, 360);
        this.galleryControlThemes.TabIndex = 0;
        this.galleryControlThemes.Text = "Theme Palace";
        // 
        // galleryControlClient1
        // 
        this.galleryControlClient1.GalleryControl = this.galleryControlThemes;
        this.galleryControlClient1.Location = new System.Drawing.Point(2, 2);
        this.galleryControlClient1.Size = new System.Drawing.Size(539, 356);
        // 
        // lblTitle
        // 
        this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
        this.lblTitle.Appearance.Options.UseFont = true;
        this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
        this.lblTitle.Location = new System.Drawing.Point(20, 20);
        this.lblTitle.Name = "lblTitle";
        this.lblTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
        this.lblTitle.Size = new System.Drawing.Size(161, 42);
        this.lblTitle.TabIndex = 1;
        this.lblTitle.Text = "Theme Palace";
        // 
        // ThemeControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.galleryControlThemes);
        this.Controls.Add(this.lblTitle);
        this.Name = "ThemeControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        ((System.ComponentModel.ISupportInitialize)(this.galleryControlThemes)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    private DevExpress.XtraBars.Ribbon.GalleryControl galleryControlThemes;
    private DevExpress.XtraBars.Ribbon.GalleryControlClient galleryControlClient1;
    private DevExpress.XtraEditors.LabelControl lblTitle;
}
