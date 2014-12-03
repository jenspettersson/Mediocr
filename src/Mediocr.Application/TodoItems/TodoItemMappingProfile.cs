using AutoMapper;
using Mediocr.Application.Infrastructure;
using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class TodoItemMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TodoItemState, TodoItemViewModel>();
            CreateMap<TodoItem, TodoItemViewModel>().ConvertUsing<DomainStateMapper<TodoItemState, TodoItemViewModel>>();
        }
    }
}