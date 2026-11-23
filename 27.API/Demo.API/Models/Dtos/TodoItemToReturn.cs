using Dome.Core.Entities;

namespace Demo.API.Models.Dtos;

public class TodoItemToReturn
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int CategoryId { get; set; }
    public string? Category { get; set; }
    public DateTime CreatedAt { get; set; }
}
