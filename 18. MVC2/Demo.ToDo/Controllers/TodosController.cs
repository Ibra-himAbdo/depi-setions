using Demo.ToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.ToDo.Controllers
{
    public class TodosController : Controller
    {

        static List<TodoItem> todos = new List<TodoItem>
        {
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Content = "Learn C#",
                IsDone = true
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Content = "Learn ASP.NET Core",
                IsDone = false
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Content = "Create a ToDo API",
                IsDone = false
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Content = "Connect to a database",
                IsDone = false
            },
            new TodoItem
            {
                Id = Guid.NewGuid(),
                Content = "Test API endpoints",
                IsDone = false
            }
        };  

        public IActionResult Index()
        {
            return View(todos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            todos.Add(todoItem);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(Guid id)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == id);
            if(item is null)
                return NotFound();

            return View(item);
        }

        public IActionResult ToggleState(Guid id)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == id);
            if (item is not null)
            {
                item.IsDone = !item.IsDone;
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(Guid id)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == id);
            if (item is null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todoItem)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == todoItem.Id);
            
            if (item is not null)
            {
                int index = todos.IndexOf(item);
                todos[index] = todoItem;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid id)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == id);
            if (item is null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        public IActionResult Delete(TodoItem todoItem)
        {
            TodoItem? item = todos.FirstOrDefault(t => t.Id == todoItem.Id);

            if (item is not null)
            {
                todos.Remove(item);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
