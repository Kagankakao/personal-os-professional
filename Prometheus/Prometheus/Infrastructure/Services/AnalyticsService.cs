using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;
using System.Text;
using System.Linq;

namespace KeganOS.Infrastructure.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ILogger _logger = Log.ForContext<AnalyticsService>();
    private readonly IJournalService _journalService;
    private readonly IAIProvider _aiProvider;
    private readonly IPixelaService _pixelaService;
    private readonly IUserService _userService;
    private readonly IKegomoDoroService _kegomoDoroService;
    private readonly IAchievementService _achievementService;

    public AnalyticsService(IJournalService journalService, IAIProvider aiProvider, 
        IPixelaService pixelaService, IUserService userService, IKegomoDoroService kegomoDoroService,
        IAchievementService achievementService)
    {
        _journalService = journalService;
        _aiProvider = aiProvider;
        _pixelaService = pixelaService;
        _userService = userService;
        _kegomoDoroService = kegomoDoroService;
        _achievementService = achievementService;
    }

    public async Task<Dictionary<DayOfWeek, double>> GetWeeklyDataAsync(User user, DateTime weekStartDate)
    {
        // Ensure we start on Monday
        var start = weekStartDate.Date;
        while (start.DayOfWeek != DayOfWeek.Monday)
        {
            start = start.AddDays(-1);
        }
        var end = start.AddDays(7); // Exclusive

        var weeklyData = new Dictionary<DayOfWeek, double>
        {
            { DayOfWeek.Monday, 0 },
            { DayOfWeek.Tuesday, 0 },
            { DayOfWeek.Wednesday, 0 },
            { DayOfWeek.Thursday, 0 },
            { DayOfWeek.Friday, 0 },
            { DayOfWeek.Saturday, 0 },
            { DayOfWeek.Sunday, 0 }
        };

        try
        {
            // Use Pixe.la for Weekly Report as requested
            if (_pixelaService != null && _pixelaService.IsConfigured(user))
            {
                var pixels = await _pixelaService.GetPixelsAsync(user, start, end);
                foreach (var pixel in pixels)
                {
                    if (weeklyData.ContainsKey(pixel.Date.DayOfWeek))
                    {
                        weeklyData[pixel.Date.DayOfWeek] += pixel.Quantity;
                    }
                }
            }
            else
            {
                // Fallback to journal if Pixe.la not configured
                var entries = await _journalService.ReadEntriesAsync(user);
                var weeklyEntries = entries.Where(e => e.Date >= start && e.Date < end);

                foreach (var entry in weeklyEntries)
                {
                    if (entry.TimeWorked.HasValue)
                    {
                        weeklyData[entry.Date.DayOfWeek] += entry.TimeWorked.Value.TotalHours;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error calculating weekly data");
        }

        return weeklyData;
    }

    public async Task<string> GenerateInsightAsync(User user, DateTime weekStartDate, string insightType)
    {
        try
        {
            var entries = await _journalService.ReadEntriesAsync(user);
            // Get last 7 days from the specified week
            var start = weekStartDate.Date;
            var end = start.AddDays(7);
            var periodEntries = entries.Where(e => e.Date >= start && e.Date < end).ToList();

            if (!periodEntries.Any())
                return "No journal entries found for this week to generate insights.";

            var sb = new StringBuilder();
            sb.AppendLine($"User: {user.DisplayName}");
            sb.AppendLine($"Week: {start:d} to {end.AddDays(-1):d}");
            sb.AppendLine("Journal Entries:");
            foreach (var entry in periodEntries)
            {
                sb.AppendLine($"- {entry.Date:d}: {entry.NoteText} ({entry.TimeWorked?.TotalHours:F1} hrs)");
            }

            string prompt = insightType switch
            {
                "Notes" => "Summarize key topics/projects. Bullet points only. Concise.",
                "Focus" => "Analyze productivity patterns. Constructive feedback. Bullet points. Concise.",
                "Life" => "Brief philosophical advice based on my week. Encouraging. Max 3 sentences.",
                "WeeklyReport" => "Generate a professional Weekly Progress Report. Sections: Key Achievements, Focus Analysis, Recommendations. Bullet points. Professional tone. Max 200 words.",
                _ => "Give me query-specific insights."
            };

            var fullPrompt = $"{prompt}\n\nContext:\n{sb}";
            return await _aiProvider.GenerateResponseAsync(fullPrompt);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error generating AI insight");
            return "Unable to generate insights at this time. Please check your internet connection and API key.";
        }
    }

    public async Task<int> CalculateCurrentStreakAsync(User user)
    {
        try
        {
            var entries = await _journalService.ReadEntriesAsync(user);
            if (!entries.Any()) return 0;

            var dates = entries.Select(e => e.Date.Date).Distinct().OrderByDescending(d => d).ToList();
            if (!dates.Any()) return 0;

            int streak = 0;
            DateTime checkDate = DateTime.Today;

            // If no entry today, check if there was one yesterday (streak still active)
            if (dates[0] < checkDate)
            {
                if (dates[0] == checkDate.AddDays(-1))
                {
                    checkDate = checkDate.AddDays(-1);
                }
                else
                {
                    return 0; // Streak broken
                }
            }

            foreach (var date in dates)
            {
                if (date == checkDate)
                {
                    streak++;
                    checkDate = checkDate.AddDays(-1);
                }
                else
                {
                    break;
                }
            }

            return streak;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error calculating streak");
            return 0;
        }
    }

    public async Task<bool> AddManualTimeAsync(User user, double hours)
    {
        try
        {
            _logger.Information("Adding manual time: {Hours} hrs for user {Name}", hours, user.DisplayName);

            // 1. Update Database (Source of Truth)
            user.TotalHours += hours;
            // Add some XP (100 XP per hour)
            user.XP += (long)(hours * 100); 
            await _userService.UpdateUserAsync(user);

            // Check Achievements
            await _achievementService.CheckAchievementsAsync(user);

            // 2. Update Pixela (Online Heatmap)
            if (_pixelaService.IsConfigured(user))
            {
                await _pixelaService.PostPixelAsync(user, DateTime.Today, hours);
            }

            // 3. Sync to KEGOMODORO (CSV)
            await _kegomoDoroService.AddManualTimeAsync(user, hours);

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Failed to add manual time");
            return false;
        }
    }
}
