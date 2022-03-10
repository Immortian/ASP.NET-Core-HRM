using CRM.Application.BusinessLogic.Employees;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CRMDBContext context) : base(context)
        {
        }

        public IEnumerable<Employee> GetActive()
        {
            return context.Employees.Where(x=>x.Active == true);
        }

        public IEnumerable<Employee> GetAuthorizer()
        {
            return context.Employees.Where(x => x.Authorizations.Any());
        }

        public IEnumerable<Employee> GetUnauthorized()
        {
            return context.Employees.Where(x => !x.Authorizations.Any());
        }

        public IEnumerable<Employee> GetWithMissingContactData()
        {
            return context.Employees.Where(x => x.ContactData.PhoneNumber != null && x.ContactData.Email != null);
        }
    }
}
