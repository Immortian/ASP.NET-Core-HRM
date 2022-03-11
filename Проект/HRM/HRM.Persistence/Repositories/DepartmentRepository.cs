using HRM.Application.BusinessLogic.Departments;
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
    }
}
