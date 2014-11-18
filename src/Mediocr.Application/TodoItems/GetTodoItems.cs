using System.Collections.Generic;
using Mediocr.Domain;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItems : IRequest<IEnumerable<TodoItem>> { }
}