using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetActive();
        IEnumerable<Employee> GetActiveByDepartmentId(int id);
        IEnumerable<Employee> GetWithMissingContactData();
        IEnumerable<Employee> GetAuthorizer();
        IEnumerable<Employee> GetUnauthorized();
    }
}
