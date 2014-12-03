using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItem, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public CreateTodoItemHandler(ITodoItemRepository repository)
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