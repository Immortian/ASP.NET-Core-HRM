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
            var handler = new RegistrationCommandHandler(context);
            await handler.Registration(request);
            return Ok(request);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(LoginCommand request)
        {
            var handler = new LoginCommandHandler(context);
            int permission = await handler.TryLogin(request);
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
