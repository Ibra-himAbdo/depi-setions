using System.ComponentModel.DataAnnotations;

namespace Demo.API.Models.Dtos
{
    public class CreateTodoItemDto
    {
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string? Title { get; init; }
        
        [MaxLength(255)]
        public string? Description { get; init; }
        public DateTime? DueDate { get; init; }
    }
}
