using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class MarkTodoItemCompleted : IRequest<TodoItem>
    {
        public string Id { get; set; }

        public MarkTodoItemCompleted(string id)
        {
            Id = id;
        }
    }
}