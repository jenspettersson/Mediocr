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
}