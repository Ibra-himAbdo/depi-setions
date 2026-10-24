namespace Demo.Todo.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }

        public string? Content { get; set; }

        public bool IsDone { get; set; }
    }
}
