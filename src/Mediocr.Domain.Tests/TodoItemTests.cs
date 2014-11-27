using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Mediocr.Domain.Tests
{
    [TestFixture]
    public class TodoItemTests
    {
        [Test]
        public void Creating_a_new_todo_item_should_raise_created_event()
        {
            var todoItem = TodoItem.Create("test");
            var events = todoItem.GetEvents();
            events.First().Should().BeOfType<TodoItemCreated>();
        }

        [Test]
        public void AssignTask_should_add_task_to_todo_item()
        {
            var todoItem = TodoItem.Create("test");
        }
    }
}
