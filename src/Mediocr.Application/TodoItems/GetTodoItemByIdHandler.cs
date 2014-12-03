using AutoMapper;
using Mediocr.Domain.TodoItems;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class GetTodoItemByIdHandler : IRequestHandler<GetTodoItemById, TodoItemViewModel>
    {
        private readonly IDocumentSession _session;

        public GetTodoItemByIdHandler(IDocumentSession session)
        {
            _session = session;
        }

        public TodoItemViewModel Handle(GetTodoItemById request)
        {
            var state = _session.Load<TodoItemState>("TodoItems/" + request.Id);
            return Mapper.Map<TodoItemViewModel>(state);
        }
    }
}