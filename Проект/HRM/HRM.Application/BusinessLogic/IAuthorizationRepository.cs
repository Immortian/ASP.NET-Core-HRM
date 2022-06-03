using HRM.Application.BuisnessLogic.Base;

namespace HRM.Application.BuisnessLogic
{
    public interface IAuthorizationRepository : IRepositoryBase<Domain.Authorization>
    {
        public bool IsUsed(string username);
    }
}
