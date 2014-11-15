using System.Linq;
using Mediocr.Domain;
using Raven.Abstractions.Extensions;
using Raven.Client.Listeners;
using Raven.Json.Linq;

namespace Mediocr.Application.Infrastructure
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

    public class DocumentStoreListener : IDocumentStoreListener
    {
     
        public bool BeforeStore(string key, object entityInstance, RavenJObject metadata, RavenJObject original)
        {
            if (!(entityInstance is IEntity))
                return true;

            var instance = (IEntity)entityInstance;
            var events = instance.GetEvents().ToArray();
            
            //events.ForEach(evt => _eventDispatcher.Dispatch(evt));

            instance.ClearEvents();

            return true;
        }

        public void AfterStore(string key, object entityInstance, RavenJObject metadata)
        {
           
        }
    }
}