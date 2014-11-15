using System.Collections.Generic;
using System.Linq;
using Mediocr.Domain;
using Raven.Abstractions.Extensions;
using Raven.Abstractions.Util.Encryptors;
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
            var entities = _entities.OfType<IEntity>();

            foreach (var entity in entities)
            {
                var evts = entity.GetEvents();

                evts.ForEach(evt => _mediator.Publish(evt));
            }


            //Todo: if pipeline contains exceptions, don't save
            _session.SaveChanges();
        }
    }
}