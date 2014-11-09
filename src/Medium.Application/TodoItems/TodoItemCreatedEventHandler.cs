using Medium.Domain;
using Raven.Client;

namespace Medium.Application.TodoItems
{
    public class TodoItemCreatedEventHandler : IEventHandler<TodoItemCreated>
    {
        private readonly IDocumentSession _session;

        public TodoItemCreatedEventHandler(IDocumentSession session)
        {
            _session = session;
        }

        public void Handle(TodoItemCreated evt)
        {
            _session.Store(evt);
        }
    }
}