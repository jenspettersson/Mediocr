using System.Collections.Generic;

namespace Mediocr.Domain
{
    public interface IEntity
    {
        string Id { get; }
        IEnumerable<IEvent> GetEvents();
        void ClearEvents();
    }
}