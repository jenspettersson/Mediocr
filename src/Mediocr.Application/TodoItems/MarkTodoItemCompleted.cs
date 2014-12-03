using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class MarkTodoItemCompleted : IRequest<TodoItemViewModel>
    {
        public string Id { get; set; }

        public MarkTodoItemCompleted(string id)
        {
            Id = id;
        }
    }
}