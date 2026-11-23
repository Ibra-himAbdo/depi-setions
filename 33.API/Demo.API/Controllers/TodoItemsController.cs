using Demo.API.Errors;
using Demo.API.Models.Dtos;
using Demo.Core.Abstractions;
using Demo.Core.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        //private readonly IGenericRepository<TodoItem> _repository;
        private readonly ITodoItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TodoItemsController(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.CustomRepository<ITodoItemRepository>();
            _unitOfWork = unitOfWork;
        }

        // create = httpPost
        // api/TodoItems
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto createTodoItemDto)
        {
            //TodoItem item = new()
            //{
            //    Title = createTodoItemDto.Title,
            //    Description = createTodoItemDto.Description,
            //    DueDate = createTodoItemDto.DueDate,
            //    CreatedAt = DateTime.UtcNow,
            //    CategoryId = createTodoItemDto.CategoryId,
            //};

            TodoItem item = createTodoItemDto.Adapt<TodoItem>();
            item.CreatedAt = DateTime.UtcNow;

            _repository.Add(item);
            await _unitOfWork.CommitAsync();

            //return Created();

            return CreatedAtAction(nameof(GetById), new { item.Id }, item);
        }

        // api/TodoItems
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _repository.GetTodoItemsWithCategories();

            //var dto = data.Select(i => new TodoItemToReturnDto()
            //{
            //    Title = i.Title
            //})

            var dto = data.Adapt<List<TodoItemToReturnDto>>();
            // 200
            return Ok(dto);
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

            _repository.Update(itemToUpdate);
            await _unitOfWork.CommitAsync();

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
            _repository.Update(item);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            int result = await _repository.DeleteByIdAsync(id);

            return result > 0 ? NoContent() : NotFound(new ApiResponse(StatusCodes.Status404NotFound, $"Item with Code '{id}' not found"));
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategoryWithNewItem(CrateItemWithCat cat)
        {
            var categoryDto = cat.categoryDto;
            var createTodoItemDto = cat.createTodoItemDto;
            await _unitOfWork.ExecuteInTransactionAsync( async () =>
            {
                Category category = new()
                {
                    Name = categoryDto.Name,
                    Description = categoryDto.Description,
                };
                _unitOfWork.Repository<Category>().Add(category);
                await _unitOfWork.CommitAsync();

                //throw new NotImplementedException();

                TodoItem item = new()
                {
                    Title = createTodoItemDto.Title,
                    Description = createTodoItemDto.Description,
                    DueDate = createTodoItemDto.DueDate,
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = category.Id,
                };
                _repository.Add(item);
                await _unitOfWork.CommitAsync();
            });


            return Ok();
        }

        public class CrateItemWithCat
        {
            public CreateCategoryDto categoryDto { get; set; }
            public CreateTodoItemDto createTodoItemDto { get; set; }
        }
    }
}