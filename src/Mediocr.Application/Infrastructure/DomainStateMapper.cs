using AutoMapper;
using Mediocr.Domain;

namespace Mediocr.Application.Infrastructure
{
    public class DomainStateMapper<TState, TResult> : ITypeConverter<IEntity<TState>, TResult>
    {
        public TResult Convert(ResolutionContext context)
        {
            var sourceValue = (IEntity<TState>)context.SourceValue;
            var state = sourceValue.GetState();
            return Mapper.Map<TResult>(state);
        }
    }
}