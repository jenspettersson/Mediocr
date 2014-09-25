namespace Medium
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> testRequest);
        void Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}