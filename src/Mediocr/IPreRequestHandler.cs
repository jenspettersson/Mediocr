namespace Medium
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}