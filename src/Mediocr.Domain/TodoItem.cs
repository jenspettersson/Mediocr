using System;

namespace Mediocr.Domain
{
    public class TodoItem : Aggregate
    {
        public string Description { get; private set; }
        public bool Completed { get; private set; }
        public DateTime CompletedAt { get; private set; }

        public TodoItem()
        {
            
        }

        private TodoItem(string description)
        {
            Description = description;
            Raise(new TodoItemCreated(this));
        }

        public static TodoItem Create(string description)
        {
            return new TodoItem(description);
        }

        public void MarkCompleted()
        {
            Completed = true;
            CompletedAt = DateTime.Now;

            Raise(new TodoItemCompleted(Id, CompletedAt));
        }
    }

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

    public class TodoItemCreated : IEvent
    {
        public TodoItem Item { get; set; }

        public TodoItemCreated(TodoItem item)
        {
            Item = item;
        }
    }
}