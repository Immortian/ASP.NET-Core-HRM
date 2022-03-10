using CRM.Application.BusinessLogic.DepartmentWorkLoads;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{
    public class DepartmentWorkLoadRepository : RepositoryBase<DepartmentWorkLoad>, IDepartmentWorkLoadRepository
    {
        public DepartmentWorkLoadRepository(CRMDBContext context) : base(context)
        {
        }

        public IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours()
        {
            return context.DepartmentWorkLoads.Where(x => x.IsEqualOrMore == true);
        }
    }
}
