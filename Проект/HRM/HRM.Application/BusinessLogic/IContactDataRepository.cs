using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IContactDataRepository : IRepositoryBase<ContactData>
    {
        IEnumerable<ContactData> GetAllWithMissing();
        IEnumerable<ContactData> GetAllWithMissingByDepartment(int id);
    }
}
