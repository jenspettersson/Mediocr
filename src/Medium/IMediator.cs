namespace Medium
{
    public interface IMediator
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
        void Publish<TEvent>(TEvent evt) where TEvent : IEvent;
    }
}