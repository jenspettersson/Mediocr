namespace Mediocr.Domain.TodoItems
{
    public class Task
    {
        public string Description { get; private set; }

        public Task(string description)
        {
            Description = description;
        }
    }
}