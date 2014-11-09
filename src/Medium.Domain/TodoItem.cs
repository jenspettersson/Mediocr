using System;

namespace Medium.Domain
{
    public class TodoItem : Aggregate
    {
        public string Description { get; private set; }
        public bool Completed { get; private set; }
        public DateTime CompletedAt { get; private set; }

        public TodoItem(string description)
        {
            Apply(new TodoItemCreated(description));
        }

        public void MarkCompleted()
        {
            Apply(new TodoItemCompleted(DateTime.Now));
        }

        public void When(TodoItemCreated evt)
        {
            Description = evt.Description;
        }

        public void When(TodoItemCompleted evt)
        {
            Completed = true;
            CompletedAt = evt.When;
        }
    }

    public class TodoItemCompleted : IEvent
    {
        public DateTime When { get; private set; }

        public TodoItemCompleted(DateTime when)
        {
            When = when;
        }
    }

    public class TodoItemCreated : IEvent
    {
        public string Description { get; private set; }

        public TodoItemCreated(string description)
        {
            Description = description;
        }
    }
}