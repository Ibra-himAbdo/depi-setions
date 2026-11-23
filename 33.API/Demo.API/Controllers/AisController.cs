using Demo.API.Errors;
using Demo.API.Models;
using Demo.API.Models.Dtos;
using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AisController : ControllerBase
    {
        private static readonly List<ChatMessage> _history = new()
        {
            new(ChatRole.System,"You are a helpfull ai assestant and your name is 'Sameh' and u can help me with some small stuff")
        };

        private readonly List<AiChatMessage> _messages = [
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
            AiChatRequest request = new AiChatRequest()
            {
                Model = "openai/gpt-oss-20b",
                Temperature = 0.2f,
                MaxCompletionTokens = 2048,
                TopP = 1,
            };

            var httpClient = _httpClientFactory.CreateClient("ai");

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
        public async Task<IActionResult> Getv2([FromBody] string userInput)
        {
            ChatOptions chatOptions = new ChatOptions()
            {
                MaxOutputTokens = 2048,
                Temperature = .2f,
                TopP = 1,
                Reasoning = new ReasoningOptions()
                {
                    Effort = Microsoft.Extensions.AI.ReasoningEffort.Medium
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
                AIFunctionFactory.Create(_unitOfWork.Repository<Category>().GetAllAsync,new(){
                    Name = "get_all_categories",
                    Description = "Retivres all categories from database"
                }),
                AIFunctionFactory.Create(_unitOfWork.Repository<Category>().GetById,new(){
                    Name = "get_category_by_id",
                    Description = "Retivres sepesific category from database by its unique id"
                }),
                 AIFunctionFactory.Create(GetTodoItemsWithCategories,new(){
                    Name = "get_all_TodoItems",
                    Description = "Retivres all TodoItems from database"
                }),
                  AIFunctionFactory.Create(MarkTaskAsCompletedAsync,new(){
                    Name = "Mark_Task_As_Completed_by_id",
                    Description = "Marks the task with the specified unique ID as completed in the database."
                })

            ];
        }

        public async Task<IReadOnlyList<TodoItemToReturnDto>> GetTodoItemsWithCategories()
        {
            IReadOnlyList<TodoItem> data = await _unitOfWork.CustomRepository<ITodoItemRepository>().GetTodoItemsWithCategories();
            var dto = data.Adapt<IReadOnlyList<TodoItemToReturnDto>>();
            // 200
            return dto;
        }

        public async Task MarkTaskAsCompletedAsync(int id)
        {
            TodoItem? item = await _unitOfWork.Repository<TodoItem>().GetById(id);
            // 404
            if (item is null) return;

            item.IsCompleted = true;
            _unitOfWork.Repository<TodoItem>().Update(item);
            await _unitOfWork.CommitAsync();
        }
    }
}