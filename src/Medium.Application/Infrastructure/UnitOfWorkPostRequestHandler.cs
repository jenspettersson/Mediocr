namespace Medium.Application.Infrastructure
{
    public class UnitOfWorkPostRequestHandler<TRequest, TResponse> : IPostRequestHandler<TRequest, TResponse>
    {
        private readonly IManageUnitOfWork _uow;

        public UnitOfWorkPostRequestHandler(IManageUnitOfWork uow)
        {
            _uow = uow;
        }

        public void Handle(TRequest request, TResponse response)
        {
            _uow.End();
        }
    }
} ;