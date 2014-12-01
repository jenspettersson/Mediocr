using Mediocr.Application.Infrastructure;
using Mediocr.Application.TodoItems;
using Mediocr.Domain.TodoItems;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using StructureMap;

namespace Mediocr.Example
{
    public class Bootstrapper : StructureMapNancyBootstrapper
    {
        protected override void ApplicationStartup(IContainer container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        protected override void ConfigureApplicationContainer(IContainer existingContainer)
        {
            // Perform registation that should have an application lifetime

            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8081",
                DefaultDatabase = "Todo"
            };

            documentStore.Conventions.FindTypeTagName =
                   type => typeof(Domain.IEvent).IsAssignableFrom(type) ?
                       type.Name :
                       DocumentConvention.DefaultTypeTagName(type);

            documentStore.Initialize();

            documentStore.Conventions.RegisterIdConvention<TodoItemState>((dbname, commands, state) => "TodoItems/" + state.Id);

            existingContainer.Configure(cfg =>
            {
                cfg.For<IDocumentStore>().Use(documentStore).Singleton();
                cfg.Scan(scanner =>
                {
                    scanner.AssemblyContainingType<CreateTodoItem>();
                    scanner.AssemblyContainingType<IMediator>();
                    scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                    scanner.AddAllTypesOf(typeof(IEventHandler<>));
                    scanner.AddAllTypesOf(typeof(IPreRequestHandler<>));
                    scanner.AddAllTypesOf(typeof(IPostRequestHandler<,>));
                    scanner.WithDefaultConventions();
                });
                
                cfg.For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                        .OpenSession());

                cfg.For<IManageUnitOfWork>()
                    .Use<RavenDbUnitOfWork>();

                cfg.For(typeof(IRequestHandler<,>))
                    .DecorateAllWith(typeof(MediatorPipeline<,>));

                cfg.For<ITodoItemRepository>().Use<TodoItemRepository>();
            });
        }

        protected override void ConfigureRequestContainer(IContainer container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
        }

        protected override void RequestStartup(IContainer container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }
    }
}