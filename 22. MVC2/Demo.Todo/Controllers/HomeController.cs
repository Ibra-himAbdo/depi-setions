using Demo.Todo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo.Todo.Controllers
{
    public class HomeController : Controller
    {
        static List<TodoItem> items =
        [
            new TodoItem { Id=Guid.NewGuid(), Content = "Item1" },
            new TodoItem { Id=Guid.NewGuid(), Content = "Item2" },
            new TodoItem { Id=Guid.NewGuid(), Content = "Item3" },
            new TodoItem { Id=Guid.NewGuid(), Content = "Item4" }
        ];

        public IActionResult Index()
        {
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem todoItem)
        {
            todoItem.Id = Guid.NewGuid();
            items.Add(todoItem);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Done(Guid Id)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == Id);
            if (seletedItem != null)
            {
                seletedItem.IsDone = !seletedItem.IsDone;
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid Id)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == Id);
            if (seletedItem != null)
            {
                return View(seletedItem);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(TodoItem todoItem)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == todoItem.Id);
            if (seletedItem != null)
            {
                int index = items.IndexOf(seletedItem);
                items[index] = todoItem;
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Detiles(Guid Id)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == Id);
            if (seletedItem != null)
            {
                return View(seletedItem);

            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid Id)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == Id);
            if (seletedItem != null)
            {
                return View(seletedItem);

            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(TodoItem todoItem)
        {
            var seletedItem = items.FirstOrDefault(e => e.Id == todoItem.Id);
            if (seletedItem != null)
            {
                items.Remove(seletedItem);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
