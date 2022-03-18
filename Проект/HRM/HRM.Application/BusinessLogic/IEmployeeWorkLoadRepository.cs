using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IEmployeeWorkLoadRepository : IRepositoryBase<EmployeeWorkLoad>
    {
        IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours();
        int SumPeriodWorkLoadHoursByDepartmentId(int periodId, int departmentId);
        int SumPeriodWorkLoadHours(int periodId);
    }
}
