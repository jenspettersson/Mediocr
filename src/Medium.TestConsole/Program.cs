using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medium.Tests;
using StructureMap;

namespace Medium.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var mediator = new Mediator(BuildContainer());

            var testResponse = mediator.Send(new TestRequest());

            _testTracker.Tracks.ToList().ForEach(Console.WriteLine);
        }

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
                    scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                    scanner.AddAllTypesOf(typeof(IEventHandler<>));
                    scanner.AddAllTypesOf(typeof(IPreRequestHandler<>));
                    scanner.AddAllTypesOf(typeof(IPostRequestHandler<,>));
                    scanner.WithDefaultConventions();
                });
                cfg.For(typeof(IRequestHandler<,>))
                    .DecorateAllWith(typeof(MediatorPipeline<,>));

                cfg.For<TestTracker>().Use(_testTracker);
            });

            return container;
        }
    }
}
