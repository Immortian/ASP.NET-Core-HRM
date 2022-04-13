using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HRMDBContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetActive()
        {
            return context.Employees
                .Where(x=>x.Active == true)
                .AsNoTracking();
        }

        public IEnumerable<Employee> GetActiveByDepartmentId(int id)
        {
            return context.Employees
                .Where(x => x.Active == true && x.DepartmentId == id)
                .AsNoTracking();
        }

        public IEnumerable<Employee> GetActiveWithNoWorkLoadByPeriodId(int periodId)
        {
            return context.Employees
                .Where(x => !x.EmployeeWorkLoads.Where(y => y.PeriodId == periodId)
                .Any());        
        }

        public IEnumerable<Employee> GetActiveWithNoWorkLoadByPeriodIdPerDepartment(int periodId, int departmentId)
        {
            return context.Employees
                .Where(x => x.DepartmentId == departmentId && !x.EmployeeWorkLoads.Where(y => y.PeriodId == periodId)
                .Any());
        }

        public IEnumerable<Employee> GetAuthorizer()
        {
            return context.Employees
                .Where(x => x.Authorizations.Any())
                .AsNoTracking();
        }

        public Employee GetByAuthCode(string authCode)
        {
            return context.Employees
                .Where(x=>x.AuthorizationCode == authCode && !x.Authorizations.Any())
                .FirstOrDefault();
        }

        public IEnumerable<Employee> GetUnauthorized()
        {
            return context.Employees
                .Where(x => !x.Authorizations.Any())
                .AsNoTracking();
        }

        public IEnumerable<Employee> GetWithMissingContactData()
        {
            return context.Employees
                .Where(x => x.ContactData.PhoneNumber != null && x.ContactData.Email != null)
                .AsNoTracking();
        }
    }
}
