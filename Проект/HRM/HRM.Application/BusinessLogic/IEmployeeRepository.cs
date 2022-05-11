using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IEnumerable<Employee> GetActive();
        IEnumerable<Employee> GetActiveByDepartmentId(int id);
        Employee GetByAuthData(string login, string password);
        Employee GetByAuthCode(string authCode);
        IEnumerable<Employee> GetWithMissingContactData();
        IEnumerable<Employee> GetAuthorizer();
        IEnumerable<Employee> GetUnauthorized();
        IEnumerable<Employee> GetActiveWithNoWorkLoadByPeriodId(int periodId);
        IEnumerable<Employee> GetActiveWithNoWorkLoadByPeriodIdPerDepartment(int periodId, int departmentId);

        string GetFullName(Employee employee);
        string GetInits(Employee employee);
        string GetAddress(Employee employee);
    }
}
