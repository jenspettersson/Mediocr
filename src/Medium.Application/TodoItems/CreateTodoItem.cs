using Medium.Domain;
using Raven.Client;

namespace Medium.Application.TodoItems
{
    public class CreateTodoItem : IRequest<TodoItem>
    {
        public string Description { get; set; }
    }

    public class CreateTodoItemHandler : IRequestHandler<CreateTodoItem, TodoItem>
    {
        private readonly IDocumentSession _session;

        public CreateTodoItemHandler(IDocumentSession session)
        {
            _session = session;
        }

        public TodoItem Handle(CreateTodoItem request)
        {
            var todoItem = new TodoItem(request.Description);

            _session.Store(todoItem);

            return todoItem;
        }
    }

    public class GetTodoItemById : IRequest<TodoItem>
    {
        public string Id { get; set; }

        public GetTodoItemById(string id)
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
}