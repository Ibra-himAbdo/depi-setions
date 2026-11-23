namespace Demo.Core.Entities;

public class TodoItem : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}