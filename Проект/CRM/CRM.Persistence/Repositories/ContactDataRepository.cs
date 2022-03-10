using CRM.Application.BusinessLogic.ContactDatum;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{
    public class ContactDataRepository : RepositoryBase<ContactData>, IContactDataRepository
    {
        public ContactDataRepository(CRMDBContext context) : base(context)
        {
        }
        /// <summary>
        /// не надо дядя
        /// </summary>
        /// <returns>ня</returns>
        public IEnumerable<ContactData> GetAllWithMissing()
        {
            return context.ContactData.Where(a => a.Employees.Any()).Where(x => x.PhoneNumber == null || x.Email == null);
        }
        /// <summary>
        /// не надо дядя
        /// </summary>
        /// <returns>ня</returns>
        /// я вообще **** эти методы, они и не нужны даже
        public IEnumerable<ContactData> GetAllWithMissingByDepartment(int id)
        {
            return context.ContactData.Where(a => a.Employees.Any()).Where(i => i.Employees.FirstOrDefault().DepartmentId == id).Where(x => x.PhoneNumber == null || x.Email == null);
        }
    }
}
