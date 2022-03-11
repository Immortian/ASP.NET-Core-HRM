using HRM.Application.BusinessLogic.EmployeeWorkLoads;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class EmployeeWorkLoadRepository : RepositoryBase<EmployeeWorkLoad>, IEmployeeWorkLoadRepository
    {
        public EmployeeWorkLoadRepository(HRMDBContext context) : base(context)
        {
        }

        public IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours()
        {
            return context.EmployeeWorkLoads.Where(x => x.WorkLoadHours >= x.WorkLoadHours);
        }
    }
}
