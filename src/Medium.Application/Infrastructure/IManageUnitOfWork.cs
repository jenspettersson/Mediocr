using System;

namespace Medium.Application
{
    public interface IManageUnitOfWork
    {
        void Begin();
        void End();
    }
}