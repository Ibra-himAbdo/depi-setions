using Demo.API.Errors;
using Demo.API.Models.Dtos;
using Dome.Core.Abstractions;
using Dome.Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        public static readonly List<TodoItem> _items = [];

        //private readonly IGenericRepository<TodoItem> _repository;
        private readonly ITodoItemRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        public TodoItemsController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.CustomRepository<ITodoItemRepository>();
            _unitOfWork = unitOfWork;
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

            _repository.Add(itemToCreate);
            await _unitOfWork.CommitAsync();

            // 201
            //return Created();

            return CreatedAtAction(nameof(GetById), new { itemToCreate.Id }, itemToCreate);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<TodoItem> data = await _repository.GetAllTodoItemsWithCategories();

            //List<TodoItemToReturn> dto = data.ConvertAll(i => new TodoItemToReturn
            //{
            //    Id = i.Id,
            //    Description = i.Description,
            //    DueDate = i.DueDate,
            //    IsCompleted = i.IsCompleted,
            //    CategoryId = i.CategoryId,
            //    CreatedAt = i.CreatedAt,
            //    Title = i.Title,
            //    Category = i.Category?.Name
            //});

            List<TodoItemToReturn> dto = data.Adapt<List<TodoItemToReturn>>();

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

            _repository.Update(itemToUpdate);
            await _unitOfWork.CommitAsync();

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

            _repository.Update(item);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            TodoItem? item = await _repository.GetByIdAsync(id);

            if (item is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with id '{id}' not found"));

            _repository.Delete(item);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCatigoryWithTodoItem(CatigoryWithTodoItem model)
        {
            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                Category category = new Category()
                {
                    Name = model.CategoryDto.Name,
                    Desctription = model.CategoryDto.Desctription,
                };
                _unitOfWork.Repository<Category>().Add(category);
                await _unitOfWork.CommitAsync();

                TodoItem itemToCreate = new()
                {
                    Description = model.ItemDto.Description,
                    DueDate = model.ItemDto.DueDate,
                    Title = model.ItemDto.Title,
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = category.Id
                };
                _repository.Add(itemToCreate);
                await _unitOfWork.CommitAsync();
            });

            return Ok();
        }
    }

    public class CatigoryWithTodoItem
    {
        public CreateCategroyDto CategoryDto { get; set; }
        public CreateTodoItemDto ItemDto { get; set; }
    }
}