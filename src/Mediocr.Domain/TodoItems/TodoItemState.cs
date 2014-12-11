using System;
using System.Collections.Generic;
using Mediocr.Domain.TodoItems.Tasks;

namespace Mediocr.Domain.TodoItems
{
    public class TodoItemState
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<Task> Tasks { get; set; }

        public TodoItemState()
        {
            Tasks = new List<Task>();
        }
    }
}