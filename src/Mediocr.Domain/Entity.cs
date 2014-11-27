using System.Collections.Generic;

namespace Mediocr.Domain
{
    public abstract class Entity<TState> : IEntity<TState>, IEventTrackedEntity
    {
        protected TState _state;
        
        private readonly List<IEvent> _events;
        protected Entity() { } 
        protected Entity(TState state)
        {
            _state = state;
            _events = new List<IEvent>();
        }

        protected void Raise(IEvent evt)
        {
            _events.Add(evt);
        }

        public IEnumerable<IEvent> GetEvents()
        {
            return _events;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }

        TState IEntity<TState>.GetState()
        {
            return _state;
        }
    }

    public interface IEventTrackedEntity
    {
        IEnumerable<IEvent> GetEvents();
        void ClearEvents();
    }
}