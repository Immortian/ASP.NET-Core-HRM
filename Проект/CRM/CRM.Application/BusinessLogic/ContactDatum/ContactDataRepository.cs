using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.ContactDatum
{
    public class ContactDataRepository : DbSet<ContactData>, IContactDataRepository
    {
        DbSet<ContactData> context;
        public ContactDataRepository(DbSet<ContactData> _context)
        {
            context = _context;
        }
        public IEnumerable<ContactData> GetAllWithMissing()
        {
            return context.Where(a => a.Employees.Any()).Where(x => x.PhoneNumber == null || x.Email == null);
        }

        public IEnumerable<ContactData> GetAllWithMissingByDepartment(int id)
        {
            return context.Where(a => a.Employees.Any()).Where(i => i.Employees.FirstOrDefault().DepartmentId == id).Where(x => x.PhoneNumber == null || x.Email == null);
        }
    }
}
