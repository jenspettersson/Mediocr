using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class PersistEvents : IEventHandler<Domain.IEvent>
    {
        private readonly IDocumentSession _session;

        public PersistEvents(IDocumentSession session)
        {
            _session = session;
        }

        public void Handle(Domain.IEvent evt)
        {
            _session.Store(evt);
        }
    }
}