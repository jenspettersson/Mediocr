namespace Mediocr
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}