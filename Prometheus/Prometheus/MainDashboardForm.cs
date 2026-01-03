using System;
using System.Windows.Forms;
using DevExpress.XtraBars.FluentDesignSystem;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using Microsoft.Extensions.DependencyInjection;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;

namespace KeganOS;

public partial class MainDashboardForm : FluentDesignForm
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserService _userService;
    private readonly ILogger _logger = Log.ForContext<MainDashboardForm>();
    
    private Control? _currentView;

    public MainDashboardForm(IServiceProvider serviceProvider, IUserService userService)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        
        // Apply dark theme before InitializeComponent
        UserLookAndFeel.Default.SetSkinStyle(SkinStyle.WXICompact);
        
        InitializeComponent();
        this.Load += MainDashboardForm_Load;
    }

    private async void MainDashboardForm_Load(object? sender, EventArgs e)
    {
        try
        {
            var user = await _userService.GetCurrentUserAsync();
            if (user != null)
            {
                this.Text = $"Prometheus - {user.DisplayName}";
            }
            
            // Load Dashboard by default
            ShowView<Views.DashboardControl>();
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to initialize MainDashboardForm");
        }
    }

    private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
    {
        if (e.Element == aceDashboard) ShowView<Views.DashboardControl>();
        else if (e.Element == acePrometheus) ShowView<Views.ChatControl>();
        else if (e.Element == aceNotes) ShowView<Views.NotesControl>();
        else if (e.Element == aceJournal) ShowView<Views.JournalControl>();
        else if (e.Element == aceAchievements) ShowView<Views.AchievementsControl>();
        else if (e.Element == aceAnalytics) ShowView<Views.AnalyticsControl>();
        else if (e.Element == aceThemes) ShowView<Views.ThemeControl>();
        else if (e.Element == aceSettings) ShowView<Views.SettingsControl>();
    }

    private void ShowView<T>() where T : Control
    {
        try
        {
            if (_currentView != null)
            {
                fluentFormContainer.Controls.Remove(_currentView);
            }
            
            var view = _serviceProvider.GetRequiredService<T>();
            view.Dock = DockStyle.Fill;
            fluentFormContainer.Controls.Add(view);
            _currentView = view;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to show view {ViewType}", typeof(T).Name);
        }
    }
}
