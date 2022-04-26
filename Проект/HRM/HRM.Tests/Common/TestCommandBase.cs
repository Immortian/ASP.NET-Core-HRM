using HRM.Application.Interfaces;
using HRM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
