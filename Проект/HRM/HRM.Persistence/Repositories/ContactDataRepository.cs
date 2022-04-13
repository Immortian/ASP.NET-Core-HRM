using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class ContactDataRepository : RepositoryBase<ContactData>, IContactDataRepository
    {
        public ContactDataRepository(HRMDBContext context) : base(context)
        {
        }
        public IEnumerable<ContactData> GetAllWithMissing()
        {
            return context.ContactData.Where(a => a.Employees.Any()).Where(x => x.PhoneNumber == null || x.Email == null).AsNoTracking();
        }
        public IEnumerable<ContactData> GetAllWithMissingByDepartment(int id)
        {
            return context.ContactData
                .Where(a => a.Employees.Any())
                .Where(i => i.Employees.FirstOrDefault().DepartmentId == id)
                .Where(x => x.PhoneNumber == null || x.Email == null)
                .AsNoTracking();
        }
    }
}
