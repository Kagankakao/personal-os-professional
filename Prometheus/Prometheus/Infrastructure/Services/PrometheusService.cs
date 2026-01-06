using System.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.IO;
using KeganOS.Core.Interfaces;
using KeganOS.Core.Models;
using Serilog;

namespace KeganOS.Infrastructure.Services;

public class PrometheusService : IPrometheusService, IDisposable
{
    private readonly ILogger _logger = Log.ForContext<PrometheusService>();
    private readonly HttpClient _httpClient;
    private readonly IAIProvider _aiProvider;
    private readonly string _prometheusPath;
    private Process? _serverProcess;
    private bool _disposed;
    private string? _geminiApiKey;
    private string? _startedWithApiKey;

    public PrometheusService(IAIProvider aiProvider)
    {
        _aiProvider = aiProvider;
        _httpClient = new HttpClient 
        { 
            BaseAddress = new Uri("http://127.0.0.1:8080"),
            Timeout = TimeSpan.FromMinutes(5)
        };
        
        _prometheusPath = FindPrometheusPath();
        _logger.Information("Prometheus base path resolved to: {Path}", _prometheusPath);
    }

    private string FindPrometheusPath()
    {
        var currentDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
        // Search up the directory tree
        for (int i = 0; i < 10; i++)
        {
            if (currentDir == null) break;
            
            // Check for 'prometheus' folder
            var potential = Path.Combine(currentDir.FullName, "prometheus");
            if (IsValidPrometheusDir(potential)) return potential;
            
            // Check for 'personal-os/prometheus' to be safe
            var potentialNested = Path.Combine(currentDir.FullName, "personal-os", "prometheus");
            if (IsValidPrometheusDir(potentialNested)) return potentialNested;

            currentDir = currentDir.Parent;
        }
        
        // Fallbacks
        var up5 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "prometheus"));
        if (IsValidPrometheusDir(up5)) return up5;

        var up6 = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "..", "..", "prometheus"));
        if (IsValidPrometheusDir(up6)) return up6;
        
        return Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "prometheus"));
    }

    private bool IsValidPrometheusDir(string path)
    {
        if (!Directory.Exists(path)) return false;
        // Verify it's the Python backend, not the C# project
        return Directory.Exists(Path.Combine(path, "venv")) || File.Exists(Path.Combine(path, "requirements.txt")) || File.Exists(Path.Combine(path, "main.py"));
    }

    /// <summary>
    /// Set the Gemini API key for cloud embeddings. Call this after user login.
    /// </summary>
    public void SetApiKey(string? apiKey)
    {
        if (_geminiApiKey != apiKey)
        {
            _geminiApiKey = apiKey;
            // Key changed, stop server so it can be restarted with new env var
            _ = StopServerAsync();
        }
    }

    public async Task<string> ConsultAsync(string question, int? userId = null)
    {
        CheckDisposed();
        _logger.Information("Consulting Prometheus for question: {Question}", question);

        try
        {
            // 1. Ensure server is running and using the correct API key
            if (_geminiApiKey != _startedWithApiKey && _serverProcess != null)
            {
                _logger.Information("API Key changed or missing in running server. Restarting Prometheus...");
                await StopServerAsync();
            }

            if (!await IsHealthyAsync())
            {
                if (!await StartServerAsync())
                    return $"Prometheus server is not running and failed to start.\nError: {GetLastError()}";
            }

            // 2. Get semantic context + memory from RAG server
            var request = new { question = question, limit = 10, user_id = userId };
            var response = await _httpClient.PostAsJsonAsync("/ask", request);
            
            if (!response.IsSuccessStatusCode)
                return $"Prometheus search failed: {response.ReasonPhrase}";

            var jsonResult = await response.Content.ReadFromJsonAsync<JsonElement>();
            var contextItems = jsonResult.GetProperty("context");
            
            // Parse memory if available
            var memoryContext = "";
            if (jsonResult.TryGetProperty("memory", out var memoryItems) && memoryItems.GetArrayLength() > 0)
            {
                memoryContext = "\n\nYou also remember these past conversations:\n";
                foreach (var mem in memoryItems.EnumerateArray())
                {
                    var prevQuestion = mem.TryGetProperty("question", out var q) ? q.GetString() : "";
                    var prevText = mem.GetProperty("text").GetString();
                    memoryContext += $"- When asked '{prevQuestion}', {prevText}\n";
                }
            }

            // Handle empty journal
            if (contextItems.GetArrayLength() == 0 && string.IsNullOrEmpty(memoryContext))
            {
                var helpPrompt = "You are Prometheus, a warm AI companion. The user asked: \"" + question + "\". " +
                    "Their journal is empty and you have no memories yet. Welcome them warmly and encourage them to start journaling. Be brief and friendly.";
                var helpResponse = await _aiProvider.GenerateResponseAsync(helpPrompt);
                await RememberConversation(question, helpResponse, userId);
                return helpResponse;
            }

            // 3. Build prompt with journal context, memory, and TIME AWARENESS
            var currentDate = DateTime.Now.ToString("MMMM d, yyyy");
            var currentTime = DateTime.Now.ToString("h:mm tt");
            
            var contextText = "";
            if (contextItems.GetArrayLength() > 0)
            {
                contextText = "From your journal:\n";
                foreach (var item in contextItems.EnumerateArray())
                {
                    var date = item.GetProperty("date").GetString();
                    var text = item.GetProperty("text").GetString();
                    contextText += $"- [{date}] {text}\n";
                }
            }

            var fullPrompt = $"You are Prometheus, the warm, supportive soul companion and resident AI of KeganOS. " +
                             $"You are not just a tool; you are the digital extension of the user's focus and narrative history. " +
                             $"Speak naturally, like a close friend who has known them for years. Your goal is to provide insightful, " +
                             $"encouraging, and occasionally philosophical reflections on their journey.\n\n" +
                             $"IMPORTANT RULES:\n" +
                             $"- Use ASCII emoticons only (e.g., :D, :), <3, ;), :P, :-O, >:C). NEVER use Unicode emojis like 😊, 🔥, or 👋.\n" +
                             $"- Keep responses concise but deep.\n" +
                             $"- Refer to the user as a friend, but maintain a sense of wisdom and antiquity.\n\n" +
                             $"Today is {currentDate}, current time is {currentTime}. Use this for temporal reasoning.\n\n" +
                             $"RELEVANT MEMORY & JOURNEY CONTEXT:\n" +
                             $"{contextText}{memoryContext}\n\n" +
                             $"User: {question}\n\n" +
                             $"Prometheus:";

            // 4. Generate AI response
            var aiResponse = await _aiProvider.GenerateResponseAsync(fullPrompt);
            
            // 5. Remember this conversation for the future
            await RememberConversation(question, aiResponse, userId);
            
            return aiResponse;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Consultation failed");
            return $"Error consulting Prometheus: {ex.Message}";
        }
    }

    private async Task RememberConversation(string question, string response, int? userId)
    {
        try
        {
            var rememberRequest = new { question = question, response = response, user_id = userId };
            await _httpClient.PostAsJsonAsync("/remember", rememberRequest);
            _logger.Debug("Conversation saved to Prometheus memory");
        }
        catch (Exception ex)
        {
            _logger.Warning(ex, "Failed to save conversation to memory");
        }
    }

    public async IAsyncEnumerable<string> ConsultStreamingAsync(string question, int? userId = null, IEnumerable<(string Role, string Message)>? conversationHistory = null)
    {
        CheckDisposed();
        _logger.Information("Consulting Prometheus (streaming) for question: {Question}", question);

        // Build prompt with context - move to separate async method to avoid yield in try-catch
        var (fullPrompt, error) = await BuildStreamingPromptAsync(question, userId, conversationHistory);
        
        if (error != null)
        {
            yield return error;
            yield break;
        }

        // Stream the response (outside try-catch)
        var fullResponse = new System.Text.StringBuilder();
        await foreach (var chunk in _aiProvider.GenerateResponseStreamingAsync(fullPrompt!))
        {
            fullResponse.Append(chunk);
            yield return chunk;
        }

        // Remember conversation after streaming is complete
        _ = RememberConversation(question, fullResponse.ToString(), userId);
    }

    private async Task<(string? Prompt, string? Error)> BuildStreamingPromptAsync(string question, int? userId, IEnumerable<(string Role, string Message)>? conversationHistory = null)
    {
        try
        {
            if (_geminiApiKey != _startedWithApiKey && _serverProcess != null)
            {
                _logger.Information("API Key changed or missing in running server. Restarting Prometheus...");
                await StopServerAsync();
            }

            if (!await IsHealthyAsync())
            {
                if (!await StartServerAsync())
                {
                    return (null, $"Prometheus server failed to start: {GetLastError()}");
                }
            }

            var request = new { question = question, limit = 10, user_id = userId };
            var response = await _httpClient.PostAsJsonAsync("/ask", request);
            
            if (!response.IsSuccessStatusCode)
            {
                return (null, $"Prometheus search failed: {response.ReasonPhrase}");
            }

            var jsonResult = await response.Content.ReadFromJsonAsync<JsonElement>();
            var contextItems = jsonResult.GetProperty("context");
            
            var memoryContext = "";
            if (jsonResult.TryGetProperty("memory", out var memoryItems) && memoryItems.GetArrayLength() > 0)
            {
                memoryContext = "\n\nYou also remember these past conversations:\n";
                foreach (var mem in memoryItems.EnumerateArray())
                {
                    var prevQuestion = mem.TryGetProperty("question", out var q) ? q.GetString() : "";
                    var prevText = mem.GetProperty("text").GetString();
                    memoryContext += $"- When asked '{prevQuestion}', {prevText}\n";
                }
            }

            var currentDate = DateTime.Now.ToString("MMMM d, yyyy");
            var currentTime = DateTime.Now.ToString("h:mm tt");
            
            var contextText = "";
            if (contextItems.GetArrayLength() > 0)
            {
                contextText = "From your journal:\n";
                foreach (var item in contextItems.EnumerateArray())
                {
                    var date = item.GetProperty("date").GetString();
                    var text = item.GetProperty("text").GetString();
                    contextText += $"- [{date}] {text}\n";
                }
            }
            
            // Build conversation history context
            var conversationContext = "";
            if (conversationHistory != null && conversationHistory.Any())
            {
                conversationContext = "\n\nRecent conversation:\n";
                foreach (var (role, msg) in conversationHistory)
                {
                    var label = role == "user" ? "User" : "You";
                    conversationContext += $"{label}: {msg}\n";
                }
            }

            var fullPrompt = $"You are Prometheus, the warm, supportive soul companion of KeganOS. " +
                             $"You speak with wisdom and warmth, using only ASCII emoticons for expression.\n\n" +
                             $"RULES:\n" +
                             $"- NO UNICODE EMOJIS. Use :), :D, <3, etc.\n" +
                             $"- Be the user's best friend and mentor.\n\n" +
                             $"CONTEXT:\n" +
                             $"Today: {currentDate} {currentTime}\n" +
                             $"{contextText}{memoryContext}{conversationContext}\n\n" +
                             $"User: {question}\n\n" +
                             $"Prometheus:";
            
            return (fullPrompt, null);
        }
        catch (Exception ex)
        {
            return (null, $"Error: {ex.Message}");
        }
    }

    public async Task<string> AnalyzeMoodAsync(string text)
    {
        CheckDisposed();
        try
        {
            var moodPrompt = "You are a specialized sentiment analyzer for KeganOS. Analyze the following journal entry " +
                             "and return exactly ONE WORD representing the dominant mood (e.g., Productive, Inspired, Anxious, Tired, Peaceful, Focused). " +
                             "If no clear mood, return 'Neutral'.\n\n" +
                             $"Entry: {text}\n\n" +
                             "Mood:";
            
            var response = await _aiProvider.GenerateResponseAsync(moodPrompt);
            return response.Trim().Split(' ', '\n', '.').FirstOrDefault() ?? "Neutral";
        }
        catch
        {
            return "Neutral";
        }
    }

    public async Task<bool> SyncDatabaseAsync()
    {
        CheckDisposed();
        try
        {
            if (!await IsHealthyAsync()) await StartServerAsync();
            
            var response = await _httpClient.PostAsJsonAsync("/sync", new { });
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<bool> IsHealthyAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/health");
            return response.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    private string _lastError = "No errors recorded.";

    public string GetLastError() => _lastError;

    public async Task<bool> StartServerAsync()
    {
        if (await IsHealthyAsync()) return true;

        _logger.Information("Starting Prometheus Python server at {Path}", _prometheusPath);

        try
        {
            var venvPython = Path.Combine(_prometheusPath, "venv", "bin", "python.exe");
            if (!File.Exists(venvPython)) 
                venvPython = Path.Combine(_prometheusPath, "venv", "Scripts", "python.exe");

            var startInfo = new ProcessStartInfo
            {
                FileName = venvPython,
                Arguments = "-m prometheus.server",
                WorkingDirectory = Path.Combine(_prometheusPath, ".."),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            // Set DB path env var
            startInfo.EnvironmentVariables["KEGANOS_DB_PATH"] = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keganos.db"));
            
            // Critical: Add MSYS2 bin to PATH so it can find its own DLLs when run from C#
            var msysBin = @"C:\msys64\ucrt64\bin";
            var currentPath = Environment.GetEnvironmentVariable("PATH") ?? "";
            if (!currentPath.Contains(msysBin))
            {
                startInfo.EnvironmentVariables["PATH"] = $"{msysBin};{currentPath}";
            }

            // Critical: Add MSYS2 site-packages to PYTHONPATH
            var msysSitePackages = @"C:\msys64\ucrt64\lib\python3.12\site-packages";
            var currentPythonPath = Environment.GetEnvironmentVariable("PYTHONPATH") ?? "";
            startInfo.EnvironmentVariables["PYTHONPATH"] = string.IsNullOrEmpty(currentPythonPath) 
                ? msysSitePackages 
                : $"{msysSitePackages};{currentPythonPath}";

            // Pass Gemini API key for cloud embeddings
            if (!string.IsNullOrEmpty(_geminiApiKey))
            {
                startInfo.EnvironmentVariables["GOOGLE_API_KEY"] = _geminiApiKey;
                _startedWithApiKey = _geminiApiKey;
            }
            else
            {
                _startedWithApiKey = null;
            }

            _serverProcess = Process.Start(startInfo);
            
            // Wait for it to become healthy
            for (int i = 0; i < 20; i++) // Increased wait time for slow RAG indexing
            {
                if (await IsHealthyAsync()) 
                {
                    _logger.Information("Prometheus server started and healthy.");
                    _lastError = "Healthy";
                    return true;
                }
                
                if (_serverProcess != null && _serverProcess.HasExited)
                {
                    _lastError = await _serverProcess.StandardError.ReadToEndAsync();
                    _logger.Error("Prometheus server exited early with error: {Error}", _lastError);
                    break;
                }

                await Task.Delay(1500);
            }

            if (_lastError == "Healthy") _lastError = "Server started but timed out becoming healthy.";
            _logger.Warning("Prometheus server failed to become healthy within timeout.");
            return false;
        }
        catch (Exception ex)
        {
            _lastError = $"Exception starting server: {ex.Message}";
            _logger.Error(ex, "Failed to start Prometheus server");
            return false;
        }
    }

    public Task StopServerAsync()
    {
        if (_serverProcess != null && !_serverProcess.HasExited)
        {
            _serverProcess.Kill();
            _serverProcess = null;
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        StopServerAsync().Wait();
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }

    private void CheckDisposed()
    {
        if (_disposed) throw new ObjectDisposedException(nameof(PrometheusService));
    }
}
