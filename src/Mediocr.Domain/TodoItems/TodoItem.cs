using System;

namespace Mediocr.Domain.TodoItems
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
}