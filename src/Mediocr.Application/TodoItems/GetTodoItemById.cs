using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemById : IRequest<TodoItem>
    {
        public int Id { get; set; }

        public GetTodoItemById(int id)
        {
            Id = id;
        }
    }
}