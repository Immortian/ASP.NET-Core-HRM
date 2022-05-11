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
                .Where(x=>x.Active == true);
        }

        public IEnumerable<Employee> GetActiveByDepartmentId(int id)
        {
            return context.Employees
                .Where(x => x.Active == true && x.DepartmentId == id);
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

        public string GetAddress(Employee employee)
        {
            return string.Join(", ",
                new string[]
                {
                    employee.Passport.City,
                    employee.Passport.Street,
                    "д." + employee.Passport.House.ToString(),
                    "к." + employee.Passport.Buinding.ToString(),
                    "кв." + employee.Passport.Apartment.ToString()
                });
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

        public Employee GetByAuthData(string login, string password)
        {
            return context.Employees
                .Where(x => x.Authorizations.FirstOrDefault().Username == login && x.Authorizations.FirstOrDefault().Password == password)
                .FirstOrDefault();
        }

        public string GetFullName(Employee employee)
        {
            return string.Join(" ", new string[]
            {
                employee.Passport.Surname,
                employee.Passport.Name,
                employee.Passport.Lastname == null? "": employee.Passport.Lastname 
            });
        }

        public string GetInits(Employee employee)
        {
            return string.Join(" ", new string[]{ employee.Passport.Surname,
                    string.Join(".", new string[]{ employee.Passport.Name[0].ToString(),
                employee.Passport.Lastname == null ? "": employee.Passport.Lastname[0].ToString()}) });
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
