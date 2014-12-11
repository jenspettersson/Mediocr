using System;
using System.Linq;
using Mediocr.Domain.TodoItems.Tasks;

namespace Mediocr.Domain.TodoItems
{
    public class TodoItem : Entity<TodoItemState>
    {
        public string Id { get { return _state.Id; } }

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

        public void AssignTask(string description)
        {
            var newTaskId = 1;
            
            if(_state.Tasks.Any())
                newTaskId = _state.Tasks.Max(x => x.Id) + 1;
            
            var task = new Task(newTaskId, description);

            _state.Tasks.Add(task);

            Raise(new TaskAssigned(_state.Id, task));
        }

        public void CompleteTask(int taskId)
        {
            var task = _state.Tasks.FirstOrDefault(x => x.Id == taskId);
            if (task == null)
                return;

            task.Complete();

            if (_state.Tasks.All(x => x.Completed))
            {
                MarkCompleted();
            }
        }
    }
}