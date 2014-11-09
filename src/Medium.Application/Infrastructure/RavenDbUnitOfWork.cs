using System;
using Raven.Client;

namespace Medium.Application
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
            _session.SaveChanges();
        }
    }
}