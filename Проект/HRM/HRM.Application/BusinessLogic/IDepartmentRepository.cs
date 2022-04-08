using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IDepartmentRepository : IRepositoryBase<Department>
    {
        bool IsEmployeeManager(int employeeId);
    }
}
