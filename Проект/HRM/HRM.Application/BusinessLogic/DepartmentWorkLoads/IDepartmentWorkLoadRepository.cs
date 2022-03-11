using HRM.Domain;

namespace HRM.Application.BusinessLogic.DepartmentWorkLoads
{
    public interface IDepartmentWorkLoadRepository
    {
        IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours();
    }
}
