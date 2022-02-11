using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.EmployeeWorkLoads
{
    public class EmployeeWorkLoadRepository : DbSet<EmployeeWorkLoad>, IEmployeeWorkLoadRepository
    {
        DbSet<EmployeeWorkLoad> context;

        public EmployeeWorkLoadRepository(DbSet<EmployeeWorkLoad> _context)
        {
            context = _context;
        }

        public IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours()
        {
            return context.Where(x => x.WorkLoadHours >= x.WorkLoadHours);
        }
    }
}
