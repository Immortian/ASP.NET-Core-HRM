using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.EmployeeWorkLoads
{
    public interface IEmployeeWorkLoadRepository
    {
        IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours();

    }
}
