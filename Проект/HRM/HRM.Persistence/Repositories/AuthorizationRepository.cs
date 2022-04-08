using HRM.Domain;
using HRM.Application.BuisnessLogic;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class AuthorizationRepository : RepositoryBase<Authorization>, IAuthorizationRepository
    {
        public AuthorizationRepository(HRMDBContext context) : base(context)
        {
        }

        public bool IsUsed(string username)
        {
            foreach(var auth in context.Authorizations)
            {
                if(auth.Username == username)
                    return true;
            }
            return false;
        }
    }
}
