using System;
using AutoMapper;
using Mediocr.Application.Infrastructure;
using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class TodoItemViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedAt { get; set; } 
    }

    public class TodoItemMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TodoItemState, TodoItemViewModel>();
            CreateMap<TodoItem, TodoItemViewModel>().ConvertUsing<DomainEntityMapper<TodoItem, TodoItemState, TodoItemViewModel>>();
        }
    }
}