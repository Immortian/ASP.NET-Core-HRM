using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.ContactDatum
{
    public interface IContactDataRepository : IDbSet<ContactData>
    {
        IEnumerable<ContactData> GetAllWithMissing();
        IEnumerable<ContactData> GetAllWithMissingByDepartment(int id);
    }
}
