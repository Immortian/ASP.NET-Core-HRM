using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.DepartmentWorkLoads
{
    public interface IDepartmentWorkLoadRepository : IDbSet<DepartmentWorkLoad>
    {
        IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours();
    }
}
