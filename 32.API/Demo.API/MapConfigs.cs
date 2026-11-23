using Demo.API.Models.Dtos;
using Demo.Core.Entities;
using Mapster;

namespace Demo.API
{
    public class MapConfigs : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TodoItem, TodoItemToReturnDto>()
                .Map(dest=> dest.Category, src => src.Category.Name);
        }
    }
}
