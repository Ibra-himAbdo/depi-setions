using System.ComponentModel.DataAnnotations;

namespace Demo.ToDo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }

        [Display(Name = "Is Done")]
        public bool IsDone { get; set; }
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}