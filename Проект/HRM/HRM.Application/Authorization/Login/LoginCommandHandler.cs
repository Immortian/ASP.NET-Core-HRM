using HRM.Application.Interfaces;

namespace HRM.Application.Authorization.Login
{
    public class LoginCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        public LoginCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<int> TryLogin(LoginCommand request)
        {
            foreach (var auth in unitOfWork.Authorization.GetAllAsync().Result)
            {
                if (request.Username == auth.Username && request.Password == auth.Password)
                {
                    if (auth.Role == "Administrator")
                        return 3;
                    else if (auth.Role == "Manager")
                        return 2;
                    else if (auth.Role == "User")
                        return 1;
                }
            }
            return 0;
        }
    }
}
