namespace Mediocr.Domain.TodoItems
{
    public class TodoItemCreated : IEvent
    {
        public TodoItemState Item { get; set; }

        public TodoItemCreated(TodoItemState item)
        {
            Item = item;
        }
    }
}