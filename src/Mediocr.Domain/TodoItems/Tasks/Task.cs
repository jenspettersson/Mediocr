namespace Mediocr.Domain.TodoItems.Tasks
{
    public class Task
    {
        public int Id { get; private set; }
        public string Description { get; private set; }
        public bool Completed { get; private set; }

        public Task(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public void Complete()
        {
            Completed = true;
        }
    }
}