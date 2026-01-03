using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;

namespace KeganOS;

partial class MainDashboardForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        
        // Main controls
        this.fluentDesignFormControl1 = new FluentDesignFormControl();
        this.fluentFormContainer = new FluentDesignFormContainer();
        this.accordionControl1 = new AccordionControl();
        
        // Navigation items - clean professional categories
        this.aceDashboard = new AccordionControlElement();
        this.acePrometheus = new AccordionControlElement();
        this.aceNotes = new AccordionControlElement();
        this.aceJournal = new AccordionControlElement();
        this.aceAchievements = new AccordionControlElement();
        this.aceAnalytics = new AccordionControlElement();
        this.aceSeparator = new AccordionControlSeparator();
        this.aceThemes = new AccordionControlElement();
        this.aceSettings = new AccordionControlElement();

        ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
        this.SuspendLayout();

        // ═══════════════════════════════════════════════════════════════════
        // FLUENT DESIGN FORM CONTROL (Modern title bar)
        // ═══════════════════════════════════════════════════════════════════
        this.fluentDesignFormControl1.FluentDesignForm = this;
        this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
        this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
        this.fluentDesignFormControl1.Size = new System.Drawing.Size(1200, 31);
        this.fluentDesignFormControl1.TabIndex = 0;
        this.fluentDesignFormControl1.TabStop = false;

        // ═══════════════════════════════════════════════════════════════════
        // ACCORDION CONTROL (Professional Navigation)
        // ═══════════════════════════════════════════════════════════════════
        this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
        this.accordionControl1.Elements.AddRange(new AccordionControlElement[] {
            this.aceDashboard,
            this.acePrometheus,
            this.aceNotes,
            this.aceJournal,
            this.aceAchievements,
            this.aceAnalytics,
            this.aceSeparator,
            this.aceThemes,
            this.aceSettings
        });
        this.accordionControl1.Location = new System.Drawing.Point(0, 31);
        this.accordionControl1.Name = "accordionControl1";
        this.accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.True;
        this.accordionControl1.ScrollBarMode = ScrollBarMode.Touch;
        this.accordionControl1.Size = new System.Drawing.Size(220, 669);
        this.accordionControl1.TabIndex = 1;
        this.accordionControl1.ViewType = AccordionControlViewType.HamburgerMenu;
        this.accordionControl1.ElementClick += AccordionControl1_ElementClick;

        // Navigation Items - Clean text-based
        this.aceDashboard.Name = "aceDashboard";
        this.aceDashboard.Style = ElementStyle.Item;
        this.aceDashboard.Text = "Dashboard";

        this.acePrometheus.Name = "acePrometheus";
        this.acePrometheus.Style = ElementStyle.Item;
        this.acePrometheus.Text = "Prometheus AI";

        this.aceNotes.Name = "aceNotes";
        this.aceNotes.Style = ElementStyle.Item;
        this.aceNotes.Text = "Neural Notes";

        this.aceJournal.Name = "aceJournal";
        this.aceJournal.Style = ElementStyle.Item;
        this.aceJournal.Text = "Daily Journal";

        this.aceAchievements.Name = "aceAchievements";
        this.aceAchievements.Style = ElementStyle.Item;
        this.aceAchievements.Text = "Achievements";

        this.aceAnalytics.Name = "aceAnalytics";
        this.aceAnalytics.Style = ElementStyle.Item;
        this.aceAnalytics.Text = "Analytics";

        this.aceSeparator.Name = "aceSeparator";

        this.aceThemes.Name = "aceThemes";
        this.aceThemes.Style = ElementStyle.Item;
        this.aceThemes.Text = "Theme Palace";

        this.aceSettings.Name = "aceSettings";
        this.aceSettings.Style = ElementStyle.Item;
        this.aceSettings.Text = "Settings";

        // ═══════════════════════════════════════════════════════════════════
        // FLUENT CONTAINER (Main content area)
        // ═══════════════════════════════════════════════════════════════════
        this.fluentFormContainer.Dock = System.Windows.Forms.DockStyle.Fill;
        this.fluentFormContainer.Location = new System.Drawing.Point(220, 31);
        this.fluentFormContainer.Name = "fluentFormContainer";
        this.fluentFormContainer.Size = new System.Drawing.Size(980, 669);
        this.fluentFormContainer.TabIndex = 2;

        // ═══════════════════════════════════════════════════════════════════
        // MAIN FORM
        // ═══════════════════════════════════════════════════════════════════
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1200, 700);
        this.ControlContainer = this.fluentFormContainer;
        this.Controls.Add(this.fluentFormContainer);
        this.Controls.Add(this.accordionControl1);
        this.Controls.Add(this.fluentDesignFormControl1);
        this.FluentDesignFormControl = this.fluentDesignFormControl1;
        this.Name = "MainDashboardForm";
        this.NavigationControl = this.accordionControl1;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Prometheus - Personal Operating System";
        
        ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
        this.ResumeLayout(false);
    }

    private FluentDesignFormControl fluentDesignFormControl1;
    private FluentDesignFormContainer fluentFormContainer;
    private AccordionControl accordionControl1;
    private AccordionControlElement aceDashboard;
    private AccordionControlElement acePrometheus;
    private AccordionControlElement aceNotes;
    private AccordionControlElement aceJournal;
    private AccordionControlElement aceAchievements;
    private AccordionControlElement aceAnalytics;
    private AccordionControlSeparator aceSeparator;
    private AccordionControlElement aceThemes;
    private AccordionControlElement aceSettings;
}
