using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Mediocr.Domain.TodoItems;
using Mediocr.Domain.TodoItems.Tasks;
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
            var state = new TodoItemState();
            var todoItem = new TodoItem(state);
            todoItem.AssignTask("Task 1");

            state.Tasks.Should().Contain(x => x.Description == "Task 1");
        }

        [Test]
        public void AssignTasked_should_have_id_generated()
        {
            var state = new TodoItemState();
            var todoItem = new TodoItem(state);
            todoItem.AssignTask("Task 1");
            todoItem.AssignTask("Task 2");

            state.Tasks.First().Id.Should().Be(1);
            state.Tasks.Last().Id.Should().Be(2);
        }

        [Test]
        public void Assigning_a_task_should_raise_TaskAssigned_event()
        {
            var state = new TodoItemState();
            var todoItem = new TodoItem(state);
            todoItem.AssignTask("Task 1");

            todoItem.GetEvents().Should().ContainItemsAssignableTo<TaskAssigned>();
        }

        [Test]
        public void CompleteTask_should_mark_task_as_completed()
        {
            var task = new Task(1, "Task 1");
            var state = new TodoItemState
            {
                Tasks = new List<Task>{ task }
            };

            var todoItem = new TodoItem(state);

            todoItem.CompleteTask(task.Id);

            task.Completed.Should().BeTrue();
        }

        [Test]
        public void Completing_all_tasks_should_make_todo_item_completed()
        {
            var state = new TodoItemState
            {
                Tasks = new List<Task>
                {
                    new Task(1, "Task 1"),
                    new Task(2, "Task 2")
                }
            };

            var todoItem = new TodoItem(state);

            todoItem.CompleteTask(1);
            todoItem.CompleteTask(2);

            state.Completed.Should().BeTrue();
        }

        [Test]
        public void Completing_all_tasks_should_raise_todo_item_completed_event()
        {
            var state = new TodoItemState
            {
                Tasks = new List<Task>
                {
                    new Task(1, "Task 1"),
                    new Task(2, "Task 2")
                }
            };

            var todoItem = new TodoItem(state);
            todoItem.CompleteTask(1);
            todoItem.CompleteTask(2);

            todoItem.GetEvents().Should().ContainItemsAssignableTo<TodoItemCompleted>();
        }
    }
}
