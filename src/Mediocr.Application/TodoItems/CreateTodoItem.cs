using Mediocr.Domain;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class CreateTodoItem : IRequest<TodoItem>
    {
        public string Description { get; set; }
    }

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

    public class GetTodoItemById : IRequest<TodoItem>
    {
        public int Id { get; set; }

        public GetTodoItemById(int id)
        {
            Id = id;
        }
    }

    public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemById, TodoItem>
    {
        private readonly IDocumentSession _session;

        public GetTodoItemByIdHandler(IDocumentSession session)
        {
            _session = session;
        }

        public TodoItem Handle(GetTodoItemById request)
        {
            return _session.Load<TodoItem>("TodoItems/" + request.Id);
        }
    }

    public class MarkTodoItemCompleted : IRequest<TodoItem>
    {
        public string Id { get; set; }

        public MarkTodoItemCompleted(string id)
        {
            Id = id;
        }
    }

    public class MarkTodItemCompletedHandler : IRequestHandler<MarkTodoItemCompleted, TodoItem>
    {
        private readonly IDocumentSession _session;

        public MarkTodItemCompletedHandler(IDocumentSession session)
        {
            _session = session;
        }

        public TodoItem Handle(MarkTodoItemCompleted request)
        {
            var todoItem = _session.Load<TodoItem>("TodoItems/" + request.Id);

            todoItem.MarkCompleted();

            return todoItem;
        }
    }
}