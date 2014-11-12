using System.Linq;
using Raven.Abstractions.Extensions;

namespace Medium.Application.Infrastructure
{
    //public class DomainEventDispatcherHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    //{
    //    private readonly IRequestHandler<TRequest, TResponse> _inner;
    //    private readonly ITrackEvents _eventsTracker;
    //    private readonly IMediator _mediator;

    //    public DomainEventDispatcherHandler(
    //        IRequestHandler<TRequest, TResponse> inner, 
    //        ITrackEvents eventsTracker,
    //        IMediator mediator)
    //    {
    //        _inner = inner;
    //        _eventsTracker = eventsTracker;
    //        _mediator = mediator;
    //    }

    //    public TResponse Handle(TRequest request)
    //    {
    //        var response = _inner.Handle(request);

    //        var events = _eventsTracker.GetEvents();
    //        if(events.Any())
    //        {
    //            events.ForEach(evt => _mediator.Publish(evt));
    //        }

    //        return response;
    //    }
    //}
}