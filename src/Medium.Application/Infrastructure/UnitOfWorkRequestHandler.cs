using System.Collections.Generic;
using System.Linq;
using Medium.Domain;
using Raven.Abstractions.Data;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Medium.Application.Infrastructure
{
    public class UnitOfWorkRequestHandler<TRequest, TResponse> : IPostRequestHandler<TRequest, TResponse>
    {
        private readonly IManageUnitOfWork _uow;

        public UnitOfWorkRequestHandler(IManageUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(TRequest request, TResponse response)
        {
            _uow.End();
        }
    }

    public class DomainEventDispatcherHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly ITrackEvents _eventsTracker;
        private readonly IDocumentSession _session;

        public DomainEventDispatcherHandler(
            IRequestHandler<TRequest, TResponse> inner, 
            ITrackEvents eventsTracker,
            IDocumentSession session)
        {
            _inner = inner;
            _eventsTracker = eventsTracker;
            _session = session;
        }

        public TResponse Handle(TRequest request)
        {
            var response = _inner.Handle(request);

            var events = _eventsTracker.GetEvents();
            if(events.Any())
            {
                events.ForEach(evt => _session.Store(evt));
                _session.SaveChanges();
            }

            return response;
        }
    }

    public interface ITrackEvents
    {
        Domain.IEvent[] GetEvents();
    }


    public class EventTracker : IDocumentStoreListener, ITrackEvents
    {
        private Domain.IEvent[] _events;

        public EventTracker()
        {
            _events = new Domain.IEvent[0];
        }

        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
        {

            return true;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
            if (!(entityInstance is IEntity))
                return;

            var instance = (IEntity) entityInstance;
            _events = instance.GetEvents().ToArray();
            instance.ClearEvents();
        }

        public Domain.IEvent[] GetEvents()
        {
            return _events;
        }
    }
}