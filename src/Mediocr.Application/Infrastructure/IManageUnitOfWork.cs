namespace Mediocr.Application.Infrastructure
{
    public interface IManageUnitOfWork
    {
        void Begin();
        void End();
    }
}