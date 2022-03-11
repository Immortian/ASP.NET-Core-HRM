using HRM.Domain;

namespace HRM.Application.BusinessLogic.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetActive();
        IEnumerable<Employee> GetWithMissingContactData();
        IEnumerable<Employee> GetAuthorizer();
        IEnumerable<Employee> GetUnauthorized();
    }
}
