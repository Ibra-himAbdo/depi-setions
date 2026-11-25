using System.Collections;

namespace Dome.Core.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public string? Desctription { get; set; }

        public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
    }
}
