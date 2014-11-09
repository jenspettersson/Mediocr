using Raven.Client;

namespace Medium.Application.Infrastructure
{
    public class RavenDbUnitOfWork : IManageUnitOfWork
    {
        private readonly IDocumentSession _session;

        public RavenDbUnitOfWork(IDocumentSession session)
        {
            _session = session;
        }

        public void Begin()
        {
            
        }

        public void End()
        {
            //Todo: if pipeline conitnues exceptions, don't save
            _session.SaveChanges();
        }
    }
}