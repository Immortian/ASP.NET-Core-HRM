using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class CompanyDataRepository : RepositoryBase<CompanyData>, ICompanyDataRepository
    {
        public CompanyDataRepository(HRMDBContext context) : base(context)
        {

        }
    }
}
