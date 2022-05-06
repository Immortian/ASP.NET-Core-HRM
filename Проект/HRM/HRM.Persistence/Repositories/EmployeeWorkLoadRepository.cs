using HRM.Application.BuisnessLogic;
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

        public IEnumerable<EmployeeWorkLoad> GetByPeriodId(int periodId)
        {
            return context.EmployeeWorkLoads
                .Where(x => x.PeriodId == periodId)
                .ToArray();
        }

        public IEnumerable<EmployeeWorkLoad> GetByPeriodIdPerDepartment(int periodId, int departmentId)
        {
            return context.EmployeeWorkLoads
                .Where(x=>x.PeriodId == periodId && x.Employee.DepartmentId == departmentId);
        }

        public IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours()
        {
            return context.EmployeeWorkLoads
                .Where(x => x.WorkLoadHours >= x.WorkLoadHours)
                .AsNoTracking();
        }

        public int SumPeriodWorkLoadHours(int periodId)
        {
            return (int)context.EmployeeWorkLoads
                .Where(x => x.PeriodId == periodId)
                .Sum(x => x.WorkLoadHours);
        }

        public int SumPeriodWorkLoadHoursByDepartmentId(int periodId, int departmentId)
        {
            return (int)context.EmployeeWorkLoads
                .Where(x => 
                    x.Employee.DepartmentId == departmentId && 
                    x.PeriodId == periodId)
                .Sum(x => x.WorkLoadHours);
        }
    }
}
