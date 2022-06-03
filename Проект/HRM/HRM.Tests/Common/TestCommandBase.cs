using HRM.Application.Interfaces;
using HRM.Persistence;
using System;

namespace HRM.Tests.Common
{
    public class TestCommandBase : IDisposable
    {
        protected readonly HRMDBContext Context;
        protected readonly IUnitOfWork unitOfWork;

        public TestCommandBase()
        {
            Context = HRMContextFactory.Create();
            unitOfWork = new UnitOfWork(Context);
        }
        public void Dispose()
        {
            HRMContextFactory.Destroy(Context);
        }
    }
}
