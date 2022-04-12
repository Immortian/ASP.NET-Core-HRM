using HRM.Application.Authorization.Login;
using HRM.Application.Authorization.Registration;
using HRM.Application.Interfaces;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IUnitOfWork context = new UnitOfWork(new HRMDBContext());

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationCommand request)
        {
            RegistrationCommandHandler reg = new RegistrationCommandHandler(context);
            await reg.Registration(request);
            return Ok(request);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginCommand request)
        {
            LoginCommandHandler reg = new LoginCommandHandler(context);
            int permission = await reg.TryLogin(request);
            if (permission == 3)
                return ""; //returnUrl to admin home page
            if (permission == 2)
                return ""; //returnUrl to admin home page
            if (permission == 1)
                return ""; //returnUrl to admin home page
            return Ok(request);
        }
    }
}
