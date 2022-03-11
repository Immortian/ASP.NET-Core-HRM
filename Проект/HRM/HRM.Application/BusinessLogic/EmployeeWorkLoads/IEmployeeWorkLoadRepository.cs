using HRM.Domain;

namespace HRM.Application.BusinessLogic.EmployeeWorkLoads
{
    public interface IEmployeeWorkLoadRepository
    {
        IEnumerable<EmployeeWorkLoad> GetWithNotEnoughHours();

    }
}
