using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Medium.Application;
using Medium.Application.Infrastructure;
using Medium.Application.TodoItems;
using Medium.Domain;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Listeners;
using Raven.Json.Linq;
using StructureMap;
using StructureMap.Pipeline;

namespace Medium.API
{
    public class ServiceActivator : IHttpControllerActivator
    {
        private IContainer _container;

        public ServiceActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(HttpRequestMessage request
            , HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controller = _container.GetInstance(controllerType) as IHttpController; 
            
            return controller;
        }
    }

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var config = GlobalConfiguration.Configuration;
            var container = BuildContainer();
            //config.DependencyResolver = new StructureMapResolver(container);

            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(container));

        }

        private Container BuildContainer()
        {
            var documentStore = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "Todo"
            };
            
            documentStore.Initialize();

            var container = new Container(cfg =>
            {
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
                cfg.For<IDocumentStore>()
                    .Use(documentStore).Singleton();

                cfg.For<IDocumentSession>()
                    .Use(ctx => ctx.GetInstance<IDocumentStore>()
                        .OpenSession());
                
                cfg.For<IManageUnitOfWork>()
                    .Use<RavenDbUnitOfWork>();

                //var handlerType = cfg.For(typeof(IRequestHandler<,>));                  
                //handlerType.DecorateAllWith(typeof(DomainEventDispatcherHandler<,>));

                cfg.For(typeof(IRequestHandler<,>))
                    .DecorateAllWith(typeof(MediatorPipeline<,>));

                var eventTracker = new EventTracker();

                cfg.For<IDocumentStoreListener>()
                    .Use(eventTracker);
                cfg.For<ITrackEvents>()
                    .Use(eventTracker);
                
                documentStore.RegisterListener(eventTracker);

                
            });

            return container;
        }
    }

    internal class TestFactory
    {
        private readonly DocumentStore _documentStore;

        public TestFactory(DocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public IDocumentSession GetSession()
        {
            return _documentStore.OpenSession();
        }
    }
}
