using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(HRMDBContext context) : base(context)
        {
        }

        public bool IsEmployeeManager(int employeeId)
        {
            foreach(var dep in context.Departments.ToList())
            {
                if(dep.ManagerId == employeeId)
                    return true;
            }
            return false;
        }
    }
}
