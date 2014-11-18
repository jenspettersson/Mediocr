using Mediocr.Domain;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
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