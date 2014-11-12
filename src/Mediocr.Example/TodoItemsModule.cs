using Medium;
using Medium.Application.TodoItems;
using Medium.Domain;
using Nancy;

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
                return "/todo-items/{id}";
            };

            Get["/{id}"] = parameters =>
            {
                var todoItem = _mediator.Send(new GetTodoItemById(parameters.id));

                return Response.AsJson(todoItem);
            };
        }
    }
}