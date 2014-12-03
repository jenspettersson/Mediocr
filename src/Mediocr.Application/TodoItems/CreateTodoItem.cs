using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItem : IRequest<TodoItem>
    {
        public string Description { get; set; }
    }
}