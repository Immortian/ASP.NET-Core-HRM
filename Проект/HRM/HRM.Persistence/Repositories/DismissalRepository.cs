using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class DismissalRepository : RepositoryBase<Dismissal>, IDismissalRepository
    {
        public DismissalRepository(HRMDBContext context) : base(context)
        {

        }
    }
}
