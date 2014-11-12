namespace Mediocr
{
    public interface IRequestHandler<in TRequest, out TResponse>
    {
        TResponse Handle(TRequest request);
    }
}