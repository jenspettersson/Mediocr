using Mediocr.Domain;
using Mediocr.Domain.TodoItems;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemById, TodoItemState>
    {
        private readonly IDocumentSession _session;

        public GetTodoItemByIdHandler(IDocumentSession session)
        {
            _session = session;
        }

        public TodoItemState Handle(GetTodoItemById request)
        {
            return _session.Load<TodoItemState>("TodoItems/" + request.Id);
        }
    }
}