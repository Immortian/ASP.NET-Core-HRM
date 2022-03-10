using CRM.Application.BusinessLogic.EmployeeWorkLoads;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{
    public class EmployeeWorkLoadRepository : RepositoryBase<EmployeeWorkLoad>, IEmployeeWorkLoadRepository
    {
        public EmployeeWorkLoadRepository(CRMDBContext context) : base(context)
        {
        }

        public IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours()
        {
            return context.EmployeeWorkLoads.Where(x => x.WorkLoadHours >= x.WorkLoadHours);
        }
    }
}
