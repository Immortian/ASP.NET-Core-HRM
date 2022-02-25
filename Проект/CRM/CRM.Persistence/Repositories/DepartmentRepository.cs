using CRM.Application.BusinessLogic.Departments;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;

namespace CRM.Persistence.Repositories
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(CRMContext context) : base(context)
        {
        }
    }
}
