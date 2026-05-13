namespace Application.Core;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UdaptedAt { get; set; }
}