using System.Collections.Generic;
using Medium.Domain;
using Raven.Client;
using Raven.Client.Linq;

namespace Medium.Application
{
    public class PreRequestLogger<TRequest> : IPreRequestHandler<TRequest>
    {
        public void Handle(TRequest request)
        {
            
        }
    }

    public class PostRequestLogger<TRequest, TResponse> : IPostRequestHandler<TRequest, TResponse>
    {
        public void Handle(TRequest request, TResponse response)
        {
            
        }
    }

    public class UnitOfWorkPostRequestHandler<TRequest, TResponse> : IPostRequestHandler<TRequest, TResponse>
    {
        private readonly IDocumentSession _session;

        public UnitOfWorkPostRequestHandler(IDocumentSession session)
        {
            _session = session;
        }

        public void Handle(TRequest request, TResponse response)
        {
            var tst = _session;
        }
    }

    public class PlaceOrder : IRequest<Order>
    {
    }

    public class GetProduct : IRequest<Product>
    {
        public int Id { get; set; }

        public GetProduct(int id)
        {
            Id = id;
        }
    }

    public class GetProductHandler : IRequestHandler<GetProduct, Product>
    {
        private readonly IDocumentSession _session;

        public GetProductHandler(IDocumentSession session)
        {
            _session = session;
        }


        public Product Handle(GetProduct request)
        {
            return _session.Load<Product>("products/" + request.Id);
        }
    }

    public class GetAllProducts : IRequest<IEnumerable<Product>> { }

    public class GetAllProductsHandler : IRequestHandler<GetAllProducts, IEnumerable<Product>>
    {
        private readonly IDocumentSession _session;

        public GetAllProductsHandler(IDocumentSession session)
        {
            _session = session;
        }

        public IEnumerable<Product> Handle(GetAllProducts request)
        {
            return _session.Query<Product>();
        }
    }
}
