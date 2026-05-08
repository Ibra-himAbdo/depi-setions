using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Client;

[ApiController]
[Route("[action]")]
public class AiController : ControllerBase
{
    public readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AiController> _logger;

    public AiController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AiController> logger)
    {
        _httpClient = httpClientFactory.CreateClient(DependencyInjection.ApplicationAI);
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("http")]
    public async Task<IActionResult> AskAsync([FromBody] string userMessage)
    {
        if (string.IsNullOrWhiteSpace(userMessage)) return BadRequest(new ApiResponse(400, "Message can't be empty"));

        ChatRequest request = new()
        {
            Model = _configuration["AI:Model"],
            Messages = new()
            {
                new() { Role = "system", Content = _configuration["AI:SystemPrompt"] },
                new() { Role = "user", Content = userMessage }
            }
        };

        try
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/openai/v1/chat/completions", request);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,
                    new ApiResponse((int)response.StatusCode, $"AI API error: {content}"));
            }

            using var doc = JsonDocument.Parse(content);

            string? message = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return Ok(message);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, new ApiResponse(503, $"Network error: {ex.Message}"));
        }
        catch (TaskCanceledException)
        {
            return StatusCode(408, new ApiResponse(408, "Request timed out"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, $"Unexpected error: {ex.Message}"));
        }
    }



}