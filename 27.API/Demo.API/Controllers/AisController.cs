using Demo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AisController : ControllerBase
    {
        private static readonly List<AiChatMessage> _messages = [
          new()
            {
                Role = MessageRole.System,
                Content = "You are a helpfull ai assestant and your name is 'Sameh' and u can help me with some small stuff"
            }
          ];
        private readonly IHttpClientFactory _httpClientFactory;

        public AisController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] string userInput)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient("ai");

            AiChatRequest request = new AiChatRequest()
            {
                Model = "openai/gpt-oss-20b",
                Temperature = 0.2f,
                MaxCompletionTokens = 2048,
                TopP = 1,
            };

            _messages.Add(new()
            {
                Role = MessageRole.User,
                Content = userInput
            });

            request.Messages = _messages;

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower)
                }
            };

            HttpResponseMessage response = await httpClient.PostAsJsonAsync("chat/completions", request, options);

            if (!response.IsSuccessStatusCode) return BadRequest(response);

            string content = await response.Content.ReadAsStringAsync();

            using JsonDocument doc = JsonDocument.Parse(content);

            string? message = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            _messages.Add(new()
            {
                Role = MessageRole.Assistant,
                Content = message
            });

            return Ok(message);

        }
    }
}
