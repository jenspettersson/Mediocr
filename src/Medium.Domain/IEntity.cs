using System.Collections.Generic;

namespace Medium.Domain
{
    public interface IEntity
    {
        string Id { get; }
        IEnumerable<IEvent> GetEvents();
        void ClearEvents();
    }
}