using Demo.API.Errors;
using Demo.API.Models.Dtos;
using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        //private readonly IGenericRepository<TodoItem> _repository;
        private readonly ITodoItemRepository _repository;

        public TodoItemsController(/*IGenericRepository<TodoItem> repository,*/ ITodoItemRepository repository)
        {
            _repository = repository;
        }

        // create = httpPost
        // api/TodoItems
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto createTodoItemDto)
        {
            TodoItem item = new()
            {
                Title = createTodoItemDto.Title,
                Description = createTodoItemDto.Description,
                DueDate = createTodoItemDto.DueDate,
                CreatedAt = DateTime.UtcNow,
                CategoryId = createTodoItemDto.CategoryId,
            };

            await _repository.AddAsync(item);

            //return Created();

            return CreatedAtAction(nameof(GetById), new { item.Id }, item);
        }

        // api/TodoItems
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // 200
            return Ok(await _repository.GetTodoItemsWithCategories());
        }

        // api/TodoItems/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            TodoItem? item = await _repository.GetTodoItemWithCategory(id);
            // 404
            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with Code '{id}' not found"));

            //200
            return Ok(item);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TodoItem itemToUpdate)
        {
            TodoItem? item = await _repository.GetById(itemToUpdate.Id);
            // 404
            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with Code '{itemToUpdate.Id}' not found"));

            item.Title = itemToUpdate.Title;
            item.Description = itemToUpdate.Description;
            item.DueDate = itemToUpdate.DueDate;
            item.IsCompleted = itemToUpdate.IsCompleted;

            await _repository.UpdateAsync(itemToUpdate);

            // 204
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> MarkTaskAsCompletedAsync([FromRoute] int id)
        {
            TodoItem? item = await _repository.GetById(id);
            // 404
            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with Code '{id}' not found"));

            item.IsCompleted = true;
            await _repository.UpdateAsync(item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            int result = await _repository.DeleteByIdAsync(id);

            return result > 0 ? NoContent() : NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with Code '{id}' not found"));
        }
    }
}