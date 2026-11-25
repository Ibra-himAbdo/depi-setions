using Demo.API.Errors;
using Demo.API.Models;
using Demo.API.Models.Dtos;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AisController : ControllerBase
    {
        private static readonly List<ChatMessage> _history =
        [
            new(ChatRole.System,"You are a helpfull ai assestant and your name is 'Sameh' and u can help me with some small stuff")
        ];
        private static readonly List<AiChatMessage> _messages = [
          new()
            {
                Role = MessageRole.System,
                Content = "You are a helpfull ai assestant and your name is 'Sameh' and u can help me with some small stuff"
            }
          ];
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatClient _chatClient;

        public AisController(IHttpClientFactory httpClientFactory, IUnitOfWork unitOfWork, IChatClient chatClient)
        {
            _httpClientFactory = httpClientFactory;
            _unitOfWork = unitOfWork;
            _chatClient = chatClient;
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


        [HttpPost("v2")]
        public async Task<IActionResult> GetV2([FromBody] string userInput)
        {
            ChatOptions chatOptions = new ChatOptions()
            {
                Temperature = 0.2f,
                MaxOutputTokens = 2048,
                TopP = 1,
                Reasoning = new ReasoningOptions()
                {
                    Effort = Microsoft.Extensions.AI.ReasoningEffort.Low
                },
                Tools = GetTools()
            };

            _history.Add(new(ChatRole.User, userInput));

            ChatResponse response = await _chatClient.GetResponseAsync(_history, chatOptions);

            if (response.Messages is not null)
                _history.AddRange(response.Messages);

            return Ok(response.Text);
        }

        private List<AITool> GetTools()
        {
            return
            [
                AIFunctionFactory.Create(
                    _unitOfWork.Repository<Category>().GetAllAsync,
                    new()
                    {
                        Name = "get_all_categories",
                        Description = "Retrieves all available categories from the database."
                    }
                ),

                AIFunctionFactory.Create(
                    _unitOfWork.Repository<Category>().GetByIdAsync,
                    new()
                    {
                        Name = "get_category_by_id",
                        Description = "Retrieves a category by its unique ID. Returns null if not found."
                    }
                ),

                AIFunctionFactory.Create(
                    GetAllTodoItmes,
                    new()
                    {
                        Name = "get_all_todo_items",
                        Description = "Retrieves all todo items including their associated categories."
                    }
                ),

                AIFunctionFactory.Create(
                    MarkAsCompletedAsync,
                    new()
                    {
                        Name = "mark_todo_as_completed",
                        Description = "Marks a todo item as completed using its ID."
                    }
                )
            ];
        }

        private async Task<List<TodoItemToReturn>> GetAllTodoItmes()
        {
            List<TodoItem> data = await _unitOfWork
                .CustomRepository<ITodoItemRepository>()
                .GetAllTodoItemsWithCategories();

            return data.Adapt<List<TodoItemToReturn>>();
        }

        public async Task MarkAsCompletedAsync(int id)
        {
            TodoItem? item = await _unitOfWork.Repository<TodoItem>().GetByIdAsync(id);

            if (item is null) return;

            item.IsCompleted = true;

            _unitOfWork.Repository<TodoItem>().Update(item);
            await _unitOfWork.CommitAsync();
        }
    }
}
