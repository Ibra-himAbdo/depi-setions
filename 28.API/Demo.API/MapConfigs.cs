using Demo.API.Models.Dtos;
using Dome.Core.Entities;
using Mapster;

namespace Demo.API;

public class MapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TodoItem, TodoItemToReturn>()
            .Map(dest => dest.Category, src => src.Category!.Name)
            .Map(dest => dest.Name, src => src.Title);
    }
}
