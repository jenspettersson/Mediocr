using System.Collections.Generic;
using System.Linq;
using Mediocr.Domain;
using Raven.Abstractions.Extensions;
using Raven.Client;

namespace Mediocr.Application.Infrastructure
{
    public class RavenDbUnitOfWork : IManageUnitOfWork
    {
        private readonly IDocumentSession _session;
        private readonly IMediator _mediator;
        private readonly List<object> _entities; 
        public RavenDbUnitOfWork(IDocumentSession session, IMediator mediator)
        {
            _session = session;
            _mediator = mediator;
            _entities = new List<object>();
        }

        public void Put(object obj)
        {
            _entities.Add(obj);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public void Begin()
        {
            
        }

        public void End()
        {
            //Todo: This should not be handled by the UoW itself, but by a separate event dispatcher

            var entities = _entities.OfType<IEventTrackedEntity>();

            foreach (var entity in entities)
            {
                var evts = entity.GetEvents().ToArray();

                evts.ForEach(evt => _mediator.Publish(evt));
                entity.ClearEvents();
            }

            //Todo: if pipeline contains exceptions, don't save
            _session.SaveChanges();

            //Todo: Dispatch events to out of transaction handlers...
        }
    }
}