using Demo.API.Errors;
using Demo.API.Models.Dtos;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        public readonly static List<TodoItem> _items = [];
        //private readonly IGenericRepository<TodoItem> _repository;
        private readonly ITodoItemRepository _repository;

        public TodoItemsController(/*IGenericRepository<TodoItem> repository,*/ ITodoItemRepository repository)
        {
            _repository = repository;
        }

        // /api/TodoItems post
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto itemDto)
        {
            TodoItem itemToCreate = new()
            {
                Description = itemDto.Description,
                DueDate = itemDto.DueDate,
                Title = itemDto.Title,
                CreatedAt = DateTime.UtcNow,
                CategoryId = itemDto.CategoryId,
            };

            await _repository.AddAsync(itemToCreate);

            // 201
            //return Created();

            return CreatedAtAction(nameof(GetById), new { itemToCreate.Id }, itemToCreate);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<TodoItem> data = await _repository.GetAllTodoItemsWithCategories();

            List<TodoItemToReturn> dto = data.ConvertAll(i => new TodoItemToReturn
            {
                Id = i.Id,
                Description = i.Description,
                DueDate = i.DueDate,
                IsCompleted = i.IsCompleted,
                CategoryId = i.CategoryId,
                CreatedAt = i.CreatedAt,
                Title = i.Title,
                Category = i.Category?.Name
            });

            // 200
            return Ok(dto);
        }

        // api/TodoItems/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            TodoItem? item = await _repository.GetTodoItemByIdWithCategory(id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            return Ok(item);
        }

        // /api/TodoItems
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoItem itemToUpdate)
        {
            TodoItem? item = await _repository.GetByIdAsync(itemToUpdate.Id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{itemToUpdate.Id}' not found"));

            item.Title = itemToUpdate.Title;
            item.Description = itemToUpdate.Description;
            item.DueDate = itemToUpdate.DueDate;
            item.IsCompleted = itemToUpdate.IsCompleted;

            await _repository.UpdateAsync(itemToUpdate);

            // 204
            return NoContent();
        }

        [HttpPatch("MarkAsCompleted/{id}")]
        public async Task<IActionResult> MarkAsCompletedAsync([FromRoute] int id)
        {
            TodoItem? item = await _repository.GetByIdAsync(id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            if (item.IsCompleted)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, $"Item with id '{id}' already Completed"));

            item.IsCompleted = true;

            await _repository.UpdateAsync(item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            TodoItem? item = await _repository.GetByIdAsync(id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            await _repository.DeleteAsync(item);

            return NoContent();
        }
    }
}
