using System;
using System.Diagnostics;
using StructureMap;
using Xunit;

namespace Medium.Tests
{
    public class NonDynamicMediatorPerformanceTest
    {
        private const int Numbers = 5000000;
        private Container _container;

        public NonDynamicMediatorPerformanceTest()
        {
            _container = new Container(cfg =>
            {
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<TestRequest>();
                    scanner.AssemblyContainingType<IMediator>();
                    scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                    scanner.WithDefaultConventions();
                });
            });
        }

        [Fact]
        public void PerfTest_NonDynamic()
        {
            var mediator = new NonDynamicMediator(_container);
            StartTest(mediator);
        }

        [Fact]
        public void PerfTest_Dynamic()
        {
            var mediator = new Mediator(_container);
            StartTest(mediator);
        }

        private static void StartTest(IMediator mediator)
        {
            Console.WriteLine("Starting with: {0}", Numbers);
            var stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < Numbers; i++)
            {
                mediator.Send(new PerfRequest());
            }

            stopwatch.Stop();

            var perMillisecond = Numbers/(double)stopwatch.ElapsedMilliseconds;

            int perSecond = (int)(perMillisecond * 1000);
            Console.WriteLine("{0} took: {1} - {2}", Numbers, stopwatch.Elapsed, perSecond);
        }
    }

   
    public class PerfRequest : IRequest<string> { }

    public class PerfRequestHandler : IRequestHandler<PerfRequest, string>
    {
        public string Handle(PerfRequest request)
        {
            return string.Empty;
        }
    }

    public class NonDynamicMediator : IMediator
    {
        private readonly IContainer _container;

        public NonDynamicMediator(IContainer container)
        {
            _container = container;
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var requestHandler = GetHandler(request);

            return requestHandler.Handle(request);
        }

        public void Publish<TEvent>(TEvent evt)
        {
            throw new NotImplementedException();
        }

        private RequestHandler<TResponse> GetHandler<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var wrapperType = typeof(RequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _container.GetInstance(handlerType);


            var wrapperHandler = Activator.CreateInstance(wrapperType, handler);
            return (RequestHandler<TResponse>)wrapperHandler;
        }

        private abstract class RequestHandler<TResult>
        {
            public abstract TResult Handle(IRequest<TResult> message);
        }

        private class RequestHandler<TCommand, TResult> : RequestHandler<TResult> where TCommand : IRequest<TResult>
        {
            private readonly IRequestHandler<TCommand, TResult> _inner;

            public RequestHandler(IRequestHandler<TCommand, TResult> inner)
            {
                _inner = inner;
            }

            public override TResult Handle(IRequest<TResult> message)
            {
                return _inner.Handle((TCommand)message);
            }
        }
    }
}