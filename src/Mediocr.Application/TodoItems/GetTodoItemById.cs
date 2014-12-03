namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemById : IRequest<TodoItemViewModel>
    {
        public int Id { get; set; }

        public GetTodoItemById(int id)
        {
            Id = id;
        }
    }
}