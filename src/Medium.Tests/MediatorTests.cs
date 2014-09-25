using System.Collections.Generic;
using FluentAssertions;
using StructureMap;
using Xunit;

namespace Medium.Tests
{
    public class MediatorTests
    {
        private readonly IMediator _mediator;
        private static TestTracker _testTracker;

        private static Container BuildContainer()
        {
            _testTracker = new TestTracker();

            var container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<TestRequest>();
                    scanner.AssemblyContainingType<IMediator>();
                    scanner.AddAllTypesOf(typeof (IRequestHandler<,>));
                    scanner.AddAllTypesOf(typeof (IEventHandler<>));
                    scanner.WithDefaultConventions();
                });
                cfg.For<TestTracker>().Use(_testTracker);
            });

            return container;
        }

        public MediatorTests()
        {
            _mediator = new Mediator(BuildContainer());
        }

        [Fact]
        public void Send_should_execute_request()
        {
            var response = _mediator.Send(new TestRequest());

            response.Message.Should().Be("Response");
        }

        [Fact]
        public void Publish_should_notify_event_handlers()
        {
            _mediator.Publish(new TestEvent());

            _testTracker.Tracks.Should().Contain(x => x == "Handler one");
            _testTracker.Tracks.Should().Contain(x => x == "Handler two");
        }
    }

    public class TestRequestHandler : IRequestHandler<TestRequest, TestResponse>
    {
        public TestResponse Handle(TestRequest request)
        {
            return new TestResponse {Message = "Response"};
        }
    }

    public class TestResponse
    {
        public string Message { get; set; }
    }

    public class TestRequest : IRequest<TestResponse>{}

    public class TestEvent : IEvent
    {
    }

    public class TestEventHandlerOne : IEventHandler<TestEvent>
    {
        private readonly TestTracker _tracker;

        public TestEventHandlerOne(TestTracker tracker)
        {
            _tracker = tracker;
        }

        public void Handle(TestEvent evt)
        {
            _tracker.Track("Handler one");
        }
    }

    public class TestEventHandlerTwo : IEventHandler<TestEvent>
    {
        private readonly TestTracker _tracker;

        public TestEventHandlerTwo(TestTracker tracker)
        {
            _tracker = tracker;

        }

        public void Handle(TestEvent evt)
        {
            _tracker.Track("Handler two");
        }
    }

    public class TestTracker
    {
        private readonly List<string> _tracks;
        public IEnumerable<string> Tracks { get { return _tracks; } }

        public TestTracker()
        {
            _tracks = new List<string>();
        }

        public void Track(string message)
        {
            _tracks.Add(message);
        }
    }
}