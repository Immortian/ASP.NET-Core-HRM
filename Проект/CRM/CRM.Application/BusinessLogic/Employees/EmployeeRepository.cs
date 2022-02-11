using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Employees
{
    public class EmployeeRepository : DbSet<Employee>, IEmployeeRepository
    {
        DbSet<Employee> context;

        public EmployeeRepository(DbSet<Employee> _context)
        {
            context = _context;
        }

        public IEnumerable<Employee> GetActive()
        {
            return context.Where(x=>x.Active == true);
        }

        public IEnumerable<Employee> GetAuthorizer()
        {
            return context.Where(x => x.Authorizations.Any());
        }

        public IEnumerable<Employee> GetUnauthorized()
        {
            return context.Where(x => !x.Authorizations.Any());
        }

        public IEnumerable<Employee> GetWithMissingContactData()
        {
            return context.Where(x => x.ContactData.PhoneNumber != null && x.ContactData.Email != null);
        }
    }
}
