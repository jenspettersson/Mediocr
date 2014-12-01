using System;

namespace Mediocr.Domain.TodoItems
{
    public class TodoItemCompleted : IEvent
    {
        public string TodoItemId { get; set; }
        public DateTime CompletedAt { get; set; }

        public TodoItemCompleted(string todoItemId, DateTime completedAt)
        {
            TodoItemId = todoItemId;
            CompletedAt = completedAt;
        }
    }
}