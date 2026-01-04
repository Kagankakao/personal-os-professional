using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using KeganOS.Core.Interfaces;
using KeganOS.Infrastructure.Data;
using KeganOS.Infrastructure.Services;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.IO;
using System.Threading.Tasks;
using KeganOS.Views;

namespace KeganOS;

static class Program
{
    private static IHost? _host;

    [STAThread]
    static void Main()
    {
        // 1. Initialize Logging
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/prometheus-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        // 2. DevExpress Visual Settings
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        
        BonusSkins.Register();
        SkinManager.EnableFormSkins();
        UserLookAndFeel.Default.SetSkinStyle("Office 2019 Black");

        // 3. Configure Host & DI
        _host = Host.CreateDefaultBuilder()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseSerilog()
            .ConfigureServices((context, services) =>
            {
                ConfigureServices(services);
            })
            .Build();

        _host.StartAsync().GetAwaiter().GetResult();

        // 4. Launch Application Flow
        try
        {
            using (var scope = _host.Services.CreateScope())
            {
                // Ensure DB is initialized
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Initialize();

                // Show Profile Selection first
                var loginForm = scope.ServiceProvider.GetRequiredService<ProfileSelectionForm>();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    var mainForm = scope.ServiceProvider.GetRequiredService<MainDashboardForm>();
                    Application.Run(mainForm);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application failed to start");
            XtraMessageBox.Show($"Fatal Error: {ex.Message}", "Critical Failure", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _host.StopAsync().GetAwaiter().GetResult();
            _host.Dispose();
            Log.CloseAndFlush();
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Database
        services.AddSingleton<AppDbContext>();

        // Core Services
        services.AddSingleton<IAIProvider, GeminiProvider>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<IJournalService, JournalService>();
        services.AddSingleton<IAchievementService, AchievementService>();
        services.AddSingleton<IAnalyticsService, AnalyticsService>();
        services.AddSingleton<IKegomoDoroService, KegomoDoroService>();
        services.AddSingleton<INoteService, NoteService>();
        services.AddSingleton<IBackupService, BackupService>();
        services.AddSingleton<IChatHistoryService, ChatHistoryService>();
        services.AddSingleton<IMotivationalMessageService, MotivationalMessageService>();
        services.AddSingleton<IPixelaService, PixelaService>();
        services.AddSingleton<IPrometheusService, PrometheusService>();
        
        // Forms & Views
        services.AddTransient<MainDashboardForm>();
        services.AddTransient<ProfileSelectionForm>();
        services.AddTransient<CreateProfileForm>();
        services.AddTransient<DashboardControl>();
        services.AddTransient<ChatControl>();
        services.AddTransient<NotesControl>();
        services.AddTransient<JournalControl>();
        services.AddTransient<AchievementsControl>();
        services.AddTransient<AnalyticsControl>();
        services.AddTransient<SettingsControl>();
        services.AddTransient<ThemeControl>();
    }
}
