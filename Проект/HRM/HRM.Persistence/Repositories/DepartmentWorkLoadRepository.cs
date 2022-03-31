using HRM.Application.BuisnessLogic;
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

        public IEnumerable<DepartmentWorkLoad> GetByPeriodId(int periodId)
        {
            return context.DepartmentWorkLoads.Where(x=>x.PeriodId == periodId);
        }

        public DepartmentWorkLoad GetByPeriodIdPerDepartment(int periodId, int departmentId)
        {
            return context.DepartmentWorkLoads.Where(x => x.PeriodId == periodId && x.DepartmentId == departmentId).FirstOrDefault();
        }

        public IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours()
        {
            return context.DepartmentWorkLoads.Where(x => x.IsEqualOrMore == true);
        }
    }
}
