using System.Collections.Generic;
using Mediocr.Domain;
using Mediocr.Domain.TodoItems;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItems : IRequest<IEnumerable<TodoItem>> { }
}