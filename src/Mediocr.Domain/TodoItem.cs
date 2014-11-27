using System;

namespace Mediocr.Domain
{
    public class TodoItem : Entity<TodoItemState>
    {
        public TodoItem(TodoItemState state) : base(state){}

        public static TodoItem Create(string description)
        {
            var state = new TodoItemState
            {
                Description = description
            };
            var todoItem = new TodoItem(state);
            todoItem.Raise(new TodoItemCreated(state));
            return todoItem;
        }

        public void MarkCompleted()
        {
            _state.Completed = true;
            _state.CompletedAt = DateTime.Now;

            Raise(new TodoItemCompleted(_state.Id, _state.CompletedAt));
        }
    }

    public class TodoItemState
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedAt { get; set; }
    }


    public class Task
    {
        public string Description { get; private set; }

        public Task(string description)
        {
            Description = description;
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
        public TodoItemState Item { get; set; }

        public TodoItemCreated(TodoItemState item)
        {
            Item = item;
        }
    }
}