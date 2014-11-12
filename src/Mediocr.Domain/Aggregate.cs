using System.Collections.Generic;

namespace Mediocr.Domain
{
    public class Aggregate : IEntity
    {
        public string Id { get; protected set; }

        private readonly List<IEvent> _events;
        
        public Aggregate()
        {
            _events = new List<IEvent>();
        }

        protected void Raise(IEvent evt)
        {
            //Mutate(evt);
            _events.Add(evt);
        }

        //private void Mutate(IEvent evt)
        //{
        //    ((dynamic)this).When((dynamic)evt);
        //}

        public IEnumerable<IEvent> GetEvents()
        {
            return _events;
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}