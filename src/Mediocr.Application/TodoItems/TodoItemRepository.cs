using Mediocr.Application.Infrastructure;
using Mediocr.Domain.TodoItems;
using Raven.Client;

namespace Mediocr.Application.TodoItems
{
    public class TodoItemRepository : EntityRepository<TodoItem, TodoItemState>, ITodoItemRepository
    {
        public TodoItemRepository(IManageUnitOfWork uow, IDocumentSession session) : base(uow, session){ }
        
        protected override TodoItem Create(TodoItemState state)
        {
            return new TodoItem(state);
        }
    }
}