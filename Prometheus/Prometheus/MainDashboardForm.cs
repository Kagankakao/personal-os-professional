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
                
                // Initialize AI Services if key is present
                if (!string.IsNullOrEmpty(user.GeminiApiKey))
                {
                    try
                    {
                        var aiProvider = _serviceProvider.GetRequiredService<IAIProvider>();
                        aiProvider.Configure(user.GeminiApiKey);
                        
                        var prometheus = _serviceProvider.GetRequiredService<IPrometheusService>();
                        prometheus.SetApiKey(user.GeminiApiKey);
                        
                        _logger.Information("AI services initialized for user {User}", user.DisplayName);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Failed to initialize AI services");
                    }
                }
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
            
            _logger.Information("Showing view: {ViewType}", typeof(T).Name);
            var view = _serviceProvider.GetRequiredService<T>();
            view.Dock = DockStyle.Fill;
            view.Visible = true;
            
            // Subscribe to logout event if this is SettingsControl
            if (view is Views.SettingsControl settingsCtrl)
            {
                settingsCtrl.LogoutRequested += SettingsControl_LogoutRequested;
            }
            
            fluentFormContainer.Controls.Add(view);
            view.BringToFront();
            _currentView = view;
            
            _logger.Information("Successfully added and displayed view: {ViewType}", typeof(T).Name);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to show view {ViewType}", typeof(T).Name);
        }
    }
    
    private void SettingsControl_LogoutRequested(object? sender, EventArgs e)
    {
        _logger.Information("Logout requested, returning to profile selection");
        
        // Close this form with Cancel result to trigger profile selection in Program.cs
        this.DialogResult = DialogResult.Retry;
        this.Close();
    }
}

