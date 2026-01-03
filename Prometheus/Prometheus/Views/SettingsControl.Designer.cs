namespace KeganOS.Views;

partial class SettingsControl
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
        this.lblWorkMin = new DevExpress.XtraEditors.LabelControl();
        this.spinWorkMin = new DevExpress.XtraEditors.SpinEdit();
        this.lblShortBreak = new DevExpress.XtraEditors.LabelControl();
        this.spinShortBreak = new DevExpress.XtraEditors.SpinEdit();
        this.lblLongBreak = new DevExpress.XtraEditors.LabelControl();
        this.spinLongBreak = new DevExpress.XtraEditors.SpinEdit();
        this.lblBackColor = new DevExpress.XtraEditors.LabelControl();
        this.colorPickBack = new DevExpress.XtraEditors.ColorPickEdit();
        this.btnSaveSettings = new DevExpress.XtraEditors.SimpleButton();
        this.groupKegomoDoro = new DevExpress.XtraEditors.GroupControl();
        ((System.ComponentModel.ISupportInitialize)(this.spinWorkMin.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinShortBreak.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinLongBreak.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.colorPickBack.Properties)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupKegomoDoro)).BeginInit();
        this.groupKegomoDoro.SuspendLayout();
        this.SuspendLayout();
        // 
        // groupKegomoDoro
        // 
        this.groupKegomoDoro.Controls.Add(this.lblWorkMin);
        this.groupKegomoDoro.Controls.Add(this.spinWorkMin);
        this.groupKegomoDoro.Controls.Add(this.lblShortBreak);
        this.groupKegomoDoro.Controls.Add(this.spinShortBreak);
        this.groupKegomoDoro.Controls.Add(this.lblLongBreak);
        this.groupKegomoDoro.Controls.Add(this.spinLongBreak);
        this.groupKegomoDoro.Controls.Add(this.lblBackColor);
        this.groupKegomoDoro.Controls.Add(this.colorPickBack);
        this.groupKegomoDoro.Dock = System.Windows.Forms.DockStyle.Top;
        this.groupKegomoDoro.Location = new System.Drawing.Point(20, 20);
        this.groupKegomoDoro.Name = "groupKegomoDoro";
        this.groupKegomoDoro.Size = new System.Drawing.Size(560, 200);
        this.groupKegomoDoro.TabIndex = 0;
        this.groupKegomoDoro.Text = "KegomoDoro Timer Settings";
        // 
        // lblWorkMin
        // 
        this.lblWorkMin.Location = new System.Drawing.Point(20, 40);
        this.lblWorkMin.Name = "lblWorkMin";
        this.lblWorkMin.Size = new System.Drawing.Size(100, 13);
        this.lblWorkMin.TabIndex = 0;
        this.lblWorkMin.Text = "Work Duration (Min):";
        // 
        // spinWorkMin
        // 
        this.spinWorkMin.EditValue = new decimal(new int[] { 25, 0, 0, 0 });
        this.spinWorkMin.Location = new System.Drawing.Point(150, 37);
        this.spinWorkMin.Name = "spinWorkMin";
        this.spinWorkMin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        this.spinWorkMin.Properties.MaxValue = new decimal(new int[] { 120, 0, 0, 0 });
        this.spinWorkMin.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        this.spinWorkMin.Size = new System.Drawing.Size(100, 20);
        this.spinWorkMin.TabIndex = 1;
        // 
        // lblShortBreak
        // 
        this.lblShortBreak.Location = new System.Drawing.Point(20, 70);
        this.lblShortBreak.Name = "lblShortBreak";
        this.lblShortBreak.Size = new System.Drawing.Size(100, 13);
        this.lblShortBreak.TabIndex = 2;
        this.lblShortBreak.Text = "Short Break (Min):";
        // 
        // spinShortBreak
        // 
        this.spinShortBreak.EditValue = new decimal(new int[] { 5, 0, 0, 0 });
        this.spinShortBreak.Location = new System.Drawing.Point(150, 67);
        this.spinShortBreak.Name = "spinShortBreak";
        this.spinShortBreak.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        this.spinShortBreak.Properties.MaxValue = new decimal(new int[] { 30, 0, 0, 0 });
        this.spinShortBreak.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        this.spinShortBreak.Size = new System.Drawing.Size(100, 20);
        this.spinShortBreak.TabIndex = 3;
        // 
        // lblLongBreak
        // 
        this.lblLongBreak.Location = new System.Drawing.Point(20, 100);
        this.lblLongBreak.Name = "lblLongBreak";
        this.lblLongBreak.Size = new System.Drawing.Size(100, 13);
        this.lblLongBreak.TabIndex = 4;
        this.lblLongBreak.Text = "Long Break (Min):";
        // 
        // spinLongBreak
        // 
        this.spinLongBreak.EditValue = new decimal(new int[] { 20, 0, 0, 0 });
        this.spinLongBreak.Location = new System.Drawing.Point(150, 97);
        this.spinLongBreak.Name = "spinLongBreak";
        this.spinLongBreak.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        this.spinLongBreak.Properties.MaxValue = new decimal(new int[] { 60, 0, 0, 0 });
        this.spinLongBreak.Properties.MinValue = new decimal(new int[] { 1, 0, 0, 0 });
        this.spinLongBreak.Size = new System.Drawing.Size(100, 20);
        this.spinLongBreak.TabIndex = 5;
        // 
        // lblBackColor
        // 
        this.lblBackColor.Location = new System.Drawing.Point(20, 130);
        this.lblBackColor.Name = "lblBackColor";
        this.lblBackColor.Size = new System.Drawing.Size(100, 13);
        this.lblBackColor.TabIndex = 6;
        this.lblBackColor.Text = "Background Color:";
        // 
        // colorPickBack
        // 
        this.colorPickBack.EditValue = System.Drawing.Color.Maroon;
        this.colorPickBack.Location = new System.Drawing.Point(150, 127);
        this.colorPickBack.Name = "colorPickBack";
        this.colorPickBack.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
        this.colorPickBack.Size = new System.Drawing.Size(100, 20);
        this.colorPickBack.TabIndex = 7;
        // 
        // btnSaveSettings
        // 
        this.btnSaveSettings.Location = new System.Drawing.Point(20, 230);
        this.btnSaveSettings.Name = "btnSaveSettings";
        this.btnSaveSettings.Size = new System.Drawing.Size(120, 35);
        this.btnSaveSettings.TabIndex = 1;
        this.btnSaveSettings.Text = "Save All Settings";
        // 
        // SettingsControl
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.btnSaveSettings);
        this.Controls.Add(this.groupKegomoDoro);
        this.Name = "SettingsControl";
        this.Padding = new System.Windows.Forms.Padding(20);
        this.Size = new System.Drawing.Size(600, 450);
        ((System.ComponentModel.ISupportInitialize)(this.spinWorkMin.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinShortBreak.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.spinLongBreak.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.colorPickBack.Properties)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.groupKegomoDoro)).EndInit();
        this.groupKegomoDoro.ResumeLayout(false);
        this.groupKegomoDoro.PerformLayout();
        this.ResumeLayout(false);

    }

    private DevExpress.XtraEditors.GroupControl groupKegomoDoro;
    private DevExpress.XtraEditors.LabelControl lblWorkMin;
    private DevExpress.XtraEditors.SpinEdit spinWorkMin;
    private DevExpress.XtraEditors.LabelControl lblShortBreak;
    private DevExpress.XtraEditors.SpinEdit spinShortBreak;
    private DevExpress.XtraEditors.LabelControl lblLongBreak;
    private DevExpress.XtraEditors.SpinEdit spinLongBreak;
    private DevExpress.XtraEditors.LabelControl lblBackColor;
    private DevExpress.XtraEditors.ColorPickEdit colorPickBack;
    private DevExpress.XtraEditors.SimpleButton btnSaveSettings;
}
