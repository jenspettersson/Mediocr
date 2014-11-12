namespace Mediocr
{
    public interface IEventHandler<in TEvent>
    {
        void Handle(TEvent evt);
    }
}