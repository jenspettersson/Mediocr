using Mediocr.Application.Infrastructure;
using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class MarkTodItemCompletedHandler : IRequestHandler<MarkTodoItemCompleted, TodoItem>
    {
        private readonly IRepository<TodoItem> _repository;

        public MarkTodItemCompletedHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public TodoItem Handle(MarkTodoItemCompleted request)
        {
            var todoItem = _repository.Load("TodoItems/" + request.Id);

            todoItem.MarkCompleted();

            return todoItem;
        }
    }
}