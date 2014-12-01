using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class MarkTodItemCompletedHandler : IRequestHandler<MarkTodoItemCompleted, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public MarkTodItemCompletedHandler(ITodoItemRepository repository)
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