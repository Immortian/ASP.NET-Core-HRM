using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.DepartmentWorkLoads
{
    public class DepartmentWorkLoadRepository : DbSet<DepartmentWorkLoad>, IDepartmentWorkLoadRepository
    {
        DbSet<DepartmentWorkLoad> context;

        public DepartmentWorkLoadRepository(DbSet<DepartmentWorkLoad> _context)
        {
            context = _context;
        }

        public IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours()
        {
            return context.Where(x => x.IsEqualOrMore == true);
        }
    }
}
