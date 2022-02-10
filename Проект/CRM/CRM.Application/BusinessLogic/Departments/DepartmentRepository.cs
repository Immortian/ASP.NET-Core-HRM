using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Departments
{
    public class DepartmentRepository : DbSet<Department>, IDepartmentRepository
    {
        DbSet<Department> context;
        public DepartmentRepository(DbSet<Department> _context)
        {
            context = _context;
        }
    }
}
