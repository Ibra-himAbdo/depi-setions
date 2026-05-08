namespace Application.Core;
public interface ISoftDelete : IEntity
{
    public bool IsDeleted { get; set; }
}
