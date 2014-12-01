using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemById : IRequest<TodoItemState>
    {
        public int Id { get; set; }

        public GetTodoItemById(int id)
        {
            Id = id;
        }
    }
}