using System.Text;
using System.Text.Json;

public interface IAiResumeAnalyzer
{
    Task<string> AnalyzeResumeAsync(string resumeText, CancellationToken ct = default);
}

public class AiResumeAnalyzer : IAiResumeAnalyzer
{
    private readonly HttpClient _httpClient;

    public AiResumeAnalyzer(IConfiguration config, HttpClient httpClient)
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(config["Ollama:Uri"]),
            Timeout = TimeSpan.FromMinutes(5)
        };
    }

    public async Task<string> AnalyzeResumeAsync(string resumeText, CancellationToken ct = default)
    {
        var requestBody = new
        {
            model = "llama3",
            prompt = $"You are an ATS Resume Analyzer. Extract key skills, years of experience, and give a short evaluation.\\n\\nResume:\\n{resumeText}",
            stream = false
        };

        var json = JsonSerializer.Serialize(requestBody);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/generate", content, ct);
        var responseString = await response.Content.ReadAsStringAsync(ct);

        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(responseString);
        return doc.RootElement.GetProperty("response").GetString();
    }
}
