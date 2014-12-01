using System.Collections.Generic;
using Mediocr.Domain;
using Mediocr.Domain.TodoItems;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemsHandler : IRequestHandler<GetTodoItems, IEnumerable<TodoItem>>
    {
        private readonly IDocumentSession _session;

        public GetTodoItemsHandler(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<TodoItem> Handle(GetTodoItems request)
        {
            return _session.Query<TodoItem>();
        }
    }
}