using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Mediocr.Domain.TodoItems;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemsHandler : IRequestHandler<GetTodoItems, IEnumerable<TodoItemViewModel>>
    {
        private readonly IDocumentSession _session;

        public GetTodoItemsHandler(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<TodoItemViewModel> Handle(GetTodoItems request)
        {
            //Todo: page...
            var items = _session.Query<TodoItemState>().ToList();

            return items.Select(Mapper.Map<TodoItemViewModel>);
        }
    }
}