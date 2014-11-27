using System.Collections.Generic;

namespace Mediocr.Domain
{
    public interface IEntity<out TState>
    {
        TState GetState();
    }
}