namespace Mediocr.Application.Infrastructure
{
    public interface IManageUnitOfWork
    {
        void Put(object obj);
        void Clear();
        void Begin();
        void End();
    }
}