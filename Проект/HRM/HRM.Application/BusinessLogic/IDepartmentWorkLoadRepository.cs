using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface IDepartmentWorkLoadRepository : IRepositoryBase<DepartmentWorkLoad>
    {
        IEnumerable<DepartmentWorkLoad> GetWithNotEnoughHours();
    }
}
