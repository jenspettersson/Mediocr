using Mediocr.Application.Infrastructure;
using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItem, TodoItem>
    {
        private readonly IRepository<TodoItem> _repository;

        public CreateTodoItemHandler(IRepository<TodoItem> repository)
        {
            _repository = repository;
        }

        public TodoItem Handle(CreateTodoItem request)
        {
            var todoItem = TodoItem.Create(request.Description);

            _repository.Add(todoItem);

            return todoItem;
        }
    }
}