using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Employees
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetActive();
        IEnumerable<Employee> GetWithMissingContactData();
        IEnumerable<Employee> GetAuthorizer();
        IEnumerable<Employee> GetUnauthorized();
    }
}
