using Mediocr.Domain;
using Raven.Client;

namespace Mediocr.Application.Infrastructure
{
    public abstract class EntityRepository<TEntity, TState> : IEntityRepository<TEntity, TState> where TEntity : class, IEntity<TState>
    {
        private readonly IManageUnitOfWork _uow;
        private readonly IDocumentSession _session;

        protected EntityRepository(IManageUnitOfWork uow, IDocumentSession session)
        {
            _uow = uow;
            _session = session;
        }

        public TEntity Load(string id)
        {
            var entity = Create(_session.Load<TState>(id));
            _uow.Put(entity);
            return entity;
        }

        public void Add(TEntity entity)
        {
            var state = entity.GetState();
			_session.Store(state);
            _uow.Put(entity);
        }


        public void Remove(TEntity entity)
        {
            _session.Delete(entity.GetState());
        }

        protected abstract TEntity Create(TState state);
    }
}