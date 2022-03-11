using HRM.Domain;

namespace HRM.Application.BusinessLogic.ContactDatum
{
    public interface IContactDataRepository
    {
        IEnumerable<ContactData> GetAllWithMissing();
        IEnumerable<ContactData> GetAllWithMissingByDepartment(int id);
    }
}
