namespace Mediocr.Domain.TodoItems.Tasks
{
    public class TaskAssigned : IEvent
    {
        public string Id { get; private set; }
        public Task Task { get; private set; }

        public TaskAssigned(string id, Task task)
        {
            Id = id;
            Task = task;
        }
    }
}