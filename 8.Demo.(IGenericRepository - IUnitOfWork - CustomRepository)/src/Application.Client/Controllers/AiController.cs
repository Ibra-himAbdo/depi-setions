using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ClientModel;
using System.Collections.Concurrent;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace Application.Client;

[ApiController]
[Route("Ai")]
[Authorize]
public class AiController : ControllerBase
{
    // A simple dictionary to store chat history per user in memory (easy way for demos)
    private static readonly ConcurrentDictionary<string, List<ChatMessage>> _userChatHistory = new();

    public readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AiController> _logger;
    private readonly IServiceProvider _serviceProvider;

    public AiController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AiController> logger, IServiceProvider serviceProvider)
    {
        _httpClient = httpClientFactory.CreateClient(DependencyInjection.ApplicationAI);
        _configuration = configuration;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    

    [HttpPost("ClearHistory")]
    public IActionResult ClearHistory()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        _userChatHistory.TryRemove(userId, out _);
        _logger.LogInformation("Chat history cleared for user: {UserId}", userId);
        return RedirectToAction("AskAdvanced", "MVCAi");
    }

    [HttpPost("AskAsync")]
    public async Task<IActionResult> AskAsync([FromBody] string userMessage)
    {
        _logger.LogInformation("AskAsync method started.");

        if (string.IsNullOrWhiteSpace(userMessage))
        {
            _logger.LogWarning("AskAsync received an empty message.");
            return BadRequest(new ApiResponse(400, "Message can't be empty"));
        }

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
            _logger.LogError(ex, "Network error occurred in AskAsync.");
            return StatusCode(503, new ApiResponse(503, $"Network error: {ex.Message}"));
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogWarning(ex, "Request timed out in AskAsync.");
            return StatusCode(408, new ApiResponse(408, "Request timed out"));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred in AskAsync.");
            return StatusCode(500, new ApiResponse(500, $"Unexpected error: {ex.Message}"));
        }
    }

    [HttpPost("AskAdvancedAsync")]
    public async Task<IActionResult> AskAdvancedAsync([FromBody] string userMessage)
    {
        _logger.LogInformation("AskAdvancedAsync method started.");

        if (string.IsNullOrWhiteSpace(userMessage))
        {
            _logger.LogWarning("AskAdvancedAsync received an empty message.");
            return BadRequest(new ApiResponse(400, "Message can't be empty"));
        }

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? Guid.NewGuid().ToString();

        try
        {
            var response = await GetAiResponse(userMessage, userId);
            string responseText = GetBestResponseText(response);
            return Ok(responseText);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred in AskAdvancedAsync.");
            return StatusCode(500, new ApiResponse(500, $"Unexpected error: {ex.Message}"));
        }
    }

    private async Task<ChatResponse> GetAiResponse(string input, string userId)
    {
        _logger.LogInformation("GetAiResponse called for user: {UserId}", userId);

        try
        {
            IChatClient client = new OpenAI.Chat.ChatClient(
                        _configuration["AI:Model"],
                        new ApiKeyCredential(_configuration["AI:API-Key"]!),
                        new OpenAI.OpenAIClientOptions()
                        {
                            Endpoint = new Uri("https://api.groq.com/openai/v1")
                        })
                    .AsIChatClient()
                    .AsBuilder()
                    .UseFunctionInvocation()
                    .Build();

            ChatOptions clientOptions = new ChatOptions()
            {
                ModelId = _configuration["AI:Model"],
                Temperature = 1,
                MaxOutputTokens = 1024,
                Tools = GetTools().ToList()
            };

            // 1. Get existing history for the user, or create a new list with the system prompt if they don't have one
            List<ChatMessage> history = _userChatHistory.GetOrAdd(userId, _ =>
                new List<ChatMessage> { new ChatMessage(ChatRole.System, _configuration["AI:SystemPrompt"]) }
            );

            // 2. Add the user's new message to the history
            history.Add(new ChatMessage(ChatRole.User, input));

            // 3. Send the entire history to the AI
            ChatResponse response = await client.GetResponseAsync(history, clientOptions);

            // 4. Save the AI's response to the history so it remembers it next time
            if (response.Messages != null)
            {
                history.AddRange(response.Messages);
            }

            if (response.Usage != null)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine(
                    $"Tokens => Input: {response.Usage.InputTokenCount}, " +
                    $"Output: {response.Usage.OutputTokenCount}, " +
                    $"Total: {response.Usage.TotalTokenCount}");

                Console.ResetColor();
            }

            _logger.LogInformation("Successfully received AI response in GetAiResponse.");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get AI response for user: {UserId}", userId);
            throw; // Rethrow to let the controller action handle the error response
        }
    }

    private static string GetBestResponseText(ChatResponse response)
    {
        if (!string.IsNullOrWhiteSpace(response.Text))
            return response.Text;

        string? textFromContents = response.Messages?
            .SelectMany(m => m.Contents)
            .OfType<TextContent>()
            .Select(c => c.Text)
            .FirstOrDefault(t => !string.IsNullOrWhiteSpace(t));

        return string.IsNullOrWhiteSpace(textFromContents)
            ? "I processed your request, but no readable text was returned."
            : textFromContents;
    }


    private IEnumerable<AITool> GetTools()
    {
        IProductRepository productRepository = _serviceProvider.GetRequiredService<IProductRepository>();
        MethodInfo getLast5ProductsFunction = typeof(IProductRepository)
            .GetMethod(nameof(IProductRepository.GetLast5ProductsAsJsonAsync),
                [typeof(CancellationToken)])!;

        yield return AIFunctionFactory.Create(
            getLast5ProductsFunction,
            productRepository,
            new AIFunctionFactoryOptions()
            {
                Name = "get_last_5_products",
                Description = "Gets the last 5 products"
            });
    }
}