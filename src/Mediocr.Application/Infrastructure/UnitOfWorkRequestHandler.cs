using System;
using System.Collections.Generic;
using System.Linq;
using Medium.Domain;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Medium.Application.Infrastructure
{
    public class Logger<TRequest, TResponse> : IPostRequestHandler<TRequest, TResponse>
    {
        public void Handle(TRequest request, TResponse response)
        {
            
        }
    }

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