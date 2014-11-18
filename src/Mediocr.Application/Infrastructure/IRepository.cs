namespace Mediocr.Application.Infrastructure
{
    public interface IRepository<T>
    {
        T Load(object id);
        void Add(T state);
    }
}