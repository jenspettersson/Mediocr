using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItem : IRequest<TodoItem>
    {
        public string Description { get; set; }
    }
}