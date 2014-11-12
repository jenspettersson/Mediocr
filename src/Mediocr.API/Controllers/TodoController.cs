using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Medium.Application;
using Medium.Application.TodoItems;
using Medium.Domain;

namespace Medium.API.Controllers
{
    public class TodoController : ApiController
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public HttpResponseMessage Post(CreateTodoItem createTodoItem)
        {
            var todoItem = _mediator.Send(createTodoItem);

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        public TodoItem Get(int id)
        {
            return _mediator.Send(new GetTodoItemById(id));
        }
    }

    public class MarkTodoItemCompletedController : ApiController
    {
        private readonly IMediator _mediator;

        public MarkTodoItemCompletedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public HttpResponseMessage Put(string id)
        {
            _mediator.Send(new MarkTodoItemCompleted(id));

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}