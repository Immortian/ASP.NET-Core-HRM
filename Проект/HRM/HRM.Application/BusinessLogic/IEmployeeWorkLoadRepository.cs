using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IEmployeeWorkLoadRepository : IRepositoryBase<EmployeeWorkLoad>
    {
        IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours();
        IEnumerable<EmployeeWorkLoad> GetByPeriodId(int periodId);
        IEnumerable<EmployeeWorkLoad> GetByPeriodIdPerDepartment(int periodId, int departmentId);
        int SumPeriodWorkLoadHoursByDepartmentId(int periodId, int departmentId);
        int SumPeriodWorkLoadHours(int periodId);
    }
}
