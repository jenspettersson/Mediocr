using System;
using Mediocr.Application.TodoItems;
using Nancy;
using Nancy.ModelBinding;

namespace Mediocr.Example
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = parameters => "Home, is 127.0.0.1!";
        }
    }

    public class TodoItemsModule : NancyModule
    {
        private readonly IMediator _mediator;

        public TodoItemsModule(IMediator mediator) : base("/todo-items")
        {
            _mediator = mediator;
            Get["/"] = parameters =>
            {
                var todoItems = _mediator.Send(new GetTodoItems());

                return Response.AsJson(todoItems);
            };

            Get["/{id}"] = parameters =>
            {
                var todoItem = _mediator.Send(new GetTodoItemById(parameters.id));

                return Response.AsJson(todoItem);
            };

            Post["/"] = parameters =>
            {
                var createTodoItem = this.Bind<CreateTodoItem>();
                var item =_mediator.Send(createTodoItem);
                return item;
            };

            Put["/{id}/complete"] = parameters =>
            {
                var id = parameters.id;
                var todoItem = _mediator.Send(new MarkTodoItemCompleted(id));
                return todoItem;
            };
        }
    }
}