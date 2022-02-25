using CRM.Application.BusinessLogic.DepartmentWorkLoads;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;

namespace CRM.Persistence.Repositories
{
    public class DepartmentWorkLoadRepository : RepositoryBase<DepartmentWorkLoad>, IDepartmentWorkLoadRepository
    {
        public DepartmentWorkLoadRepository(CRMContext context) : base(context)
        {
        }

        public IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours()
        {
            return context.DepartmentWorkLoads.Where(x => x.IsEqualOrMore == true);
        }
    }
}
