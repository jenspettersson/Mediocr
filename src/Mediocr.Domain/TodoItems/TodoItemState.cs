using System;

namespace Mediocr.Domain.TodoItems
{
    public class TodoItemState
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}