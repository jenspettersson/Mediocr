using AutoMapper;
using Mediocr.Application.TodoItems;

namespace Mediocr.Application
{
    public static class ApplicationBootstrapper
    {
        public static void Init()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<TodoItemMappingProfile>();
            });
        }
    }
}