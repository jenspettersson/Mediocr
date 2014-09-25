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
            dynamic instance = _container.GetInstance(handlerType);

            return (TResponse)instance.Handle((dynamic)request);
        }

        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var eventHandlers = _container.GetAllInstances<IEventHandler<TEvent>>();

            foreach (var handler in eventHandlers)
            {
                handler.Handle(evt);
            }
        }
    }
}