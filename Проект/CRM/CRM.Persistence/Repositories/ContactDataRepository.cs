using CRM.Application.BusinessLogic.ContactDatum;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;

namespace CRM.Persistence.Repositories
{
    public class ContactDataRepository : RepositoryBase<ContactData>, IContactDataRepository
    {
        public ContactDataRepository(CRMContext context) : base(context)
        {
        }

        public IEnumerable<ContactData> GetAllWithMissing()
        {
            return context.ContactData.Where(a => a.Employees.Any()).Where(x => x.PhoneNumber == null || x.Email == null);
        }

        public IEnumerable<ContactData> GetAllWithMissingByDepartment(int id)
        {
            return context.ContactData.Where(a => a.Employees.Any()).Where(i => i.Employees.FirstOrDefault().DepartmentId == id).Where(x => x.PhoneNumber == null || x.Email == null);
        }
    }
}
