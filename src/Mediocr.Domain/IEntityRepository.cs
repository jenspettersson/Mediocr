namespace Mediocr.Domain
{
    public interface IEntityRepository<TEntity, TState> where TEntity : class, IEntity<TState>
    {
        TEntity Load(string id);
        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}