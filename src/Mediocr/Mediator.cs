using StructureMap;

namespace Medium
{
    public class Mediator : IMediator
    {
        private readonly IContainer _container;

        public Mediator(IContainer container)
        {
            _container = container;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            using (var childContainer = _container.GetNestedContainer())
            {
                dynamic instance = childContainer.GetInstance(handlerType);

                return (TResponse) instance.Handle((dynamic) request);
            }
        }

        public void Publish<TEvent>(TEvent evt)
        {
            var handlerType = typeof(IEventHandler<>).MakeGenericType(evt.GetType());

            var eventHandlers = _container.GetAllInstances(handlerType);

            foreach (dynamic handler in eventHandlers)
            {
                handler.Handle((dynamic)evt);
            }
        }
    }
}