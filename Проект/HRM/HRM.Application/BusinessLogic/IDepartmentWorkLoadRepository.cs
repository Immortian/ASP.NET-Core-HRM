using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IDepartmentWorkLoadRepository : IRepositoryBase<DepartmentWorkLoad>
    {
        IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours();
        IEnumerable<DepartmentWorkLoad> GetByPeriodId(int periodId);
        DepartmentWorkLoad GetByPeriodIdPerDepartment(int periodId, int departmentId);
    }
}
