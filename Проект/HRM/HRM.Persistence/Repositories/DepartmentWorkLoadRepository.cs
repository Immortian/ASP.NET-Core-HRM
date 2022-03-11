using HRM.Application.BusinessLogic.DepartmentWorkLoads;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class DepartmentWorkLoadRepository : RepositoryBase<DepartmentWorkLoad>, IDepartmentWorkLoadRepository
    {
        public DepartmentWorkLoadRepository(HRMDBContext context) : base(context)
        {
        }

        public IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours()
        {
            return context.DepartmentWorkLoads.Where(x => x.IsEqualOrMore == true);
        }
    }
}
