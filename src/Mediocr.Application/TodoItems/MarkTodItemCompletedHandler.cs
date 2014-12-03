using AutoMapper;
using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class MarkTodItemCompletedHandler : IRequestHandler<MarkTodoItemCompleted, TodoItemViewModel>
    {
        private readonly ITodoItemRepository _repository;

        public MarkTodItemCompletedHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public TodoItemViewModel Handle(MarkTodoItemCompleted request)
        {
            var todoItem = _repository.Load("TodoItems/" + request.Id);

            todoItem.MarkCompleted();

            return Mapper.Map<TodoItemViewModel>(todoItem);
        }
    }
}