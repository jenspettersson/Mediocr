using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItem : IRequest<TodoItemViewModel>
    {
        public string Description { get; set; }
    }
}