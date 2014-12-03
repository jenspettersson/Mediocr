using AutoMapper;
using Mediocr.Domain;

namespace Mediocr.Application.Infrastructure
{
    public class DomainEntityMapper<TEntity, TState, TResult> : ITypeConverter<TEntity, TResult>
    {
        public TResult Convert(ResolutionContext context)
        {
            var sourceValue = context.SourceValue;
            var state = ((IEntity<TState>)sourceValue).GetState();
            return Mapper.Map<TResult>(state);
        }
    }
}