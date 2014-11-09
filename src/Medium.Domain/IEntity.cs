using System.Collections.Generic;

namespace Medium.Domain
{
    public interface IEntity
    {
        IEnumerable<IEvent> GetUncommitedEvents();
        void ClearEvents();
    }
}