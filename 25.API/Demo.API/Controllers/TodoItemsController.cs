using Demo.API.Errors;
using Demo.API.Models;
using Demo.API.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        public readonly static List<TodoItem> _items = new List<TodoItem>
        {
            new TodoItem
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Title = "Learn ASP.NET Core",
                Description = "Study Web API fundamentals and middleware.",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(2),
                CreatedAt = DateTime.Now
            },
            new TodoItem
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Title = "Build Todo API",
                Description = "Implement CRUD operations using ASP.NET Core.",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(5),
                CreatedAt = DateTime.Now
            },
            new TodoItem
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Title = "Learn Entity Framework Core",
                Description = "Practice LINQ queries, relationships, and migrations.",
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(-2),
                CreatedAt = DateTime.Now.AddDays(-7)
            },
            new TodoItem
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Title = "Prepare for Job Interview",
                Description = "Review C#, OOP, SOLID, and design patterns.",
                IsCompleted = false,
                DueDate = DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now
            },
            new TodoItem
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Title = "Read Clean Code",
                Description = "Read chapters 1 to 3 and take notes.",
                IsCompleted = true,
                DueDate = DateTime.Now.AddDays(-1),
                CreatedAt = DateTime.Now.AddDays(-10)
            }
        };

        // /api/TodoItems post
        [HttpPost]
        public IActionResult Create([FromBody] CreateTodoItemDto itemDto)
        {
            TodoItem itemToCreate = new()
            {
                Id = Guid.NewGuid(),
                Description = itemDto.Description,
                DueDate = itemDto.DueDate,
                Title = itemDto.Title,
                CreatedAt = DateTime.UtcNow
            };

            _items.Add(itemToCreate);

            // 201
            //return Created();

            return CreatedAtAction(nameof(GetById), new { itemToCreate.Id }, itemToCreate);
        }

        [HttpGet]
        public IActionResult Get()
        {
            // 200
            return Ok(_items);
        }

        // api/TodoItems/id
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            TodoItem? item = _items.FirstOrDefault(i => i.Id == id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            return Ok(item);
        }

        // /api/TodoItems
        [HttpPut]
        public IActionResult Update([FromBody] TodoItem itemToUpdate)
        {
            TodoItem? item = _items.FirstOrDefault(i => i.Id == itemToUpdate.Id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{itemToUpdate.Id}' not found"));

            item.Title = itemToUpdate.Title;
            item.Description = itemToUpdate.Description;
            item.DueDate = itemToUpdate.DueDate;
            item.IsCompleted = itemToUpdate.IsCompleted;

            // 204
            return NoContent();
        }

        [HttpPatch("MarkAsCompleted/{id}")]
        public IActionResult MarkAsCompleted([FromRoute] Guid id)
        {
            TodoItem? item = _items.FirstOrDefault(i => i.Id == id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            if(item.IsCompleted)
                return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, $"Item with id '{id}' already Completed"));

            item.IsCompleted = true;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            TodoItem? item = _items.FirstOrDefault(i => i.Id == id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            _items.Remove(item);

            return NoContent();
        }
    }
}
