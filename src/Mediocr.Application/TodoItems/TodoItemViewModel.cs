using System;

namespace Mediocr.Application.TodoItems
{
    public class TodoItemViewModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedAt { get; set; } 
    }
}