using HRM.Application.Authorization.Login;
using HRM.Application.Authorization.Registration;
using HRM.Application.Interfaces;
using HRM.Domain;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers.Authorization
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IUnitOfWork context;
        public AuthorizationController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(RegistrationCommand request)
        {
            var handler = new RegistrationCommandHandler(context);
            await handler.Registration(request);
            return Ok(request);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<int>> Login(LoginCommand request)
        {
            var handler = new LoginCommandHandler(context);
            int permission = await handler.TryLogin(request);
            if (permission == 3)
                return 3; //returnUrl to admin home page
            if (permission == 2)
                return 2; //returnUrl to admin home page
            if (permission == 1)
                return 1; //returnUrl to admin home page
            else
                return 0;
        }

        [HttpPost("Authorize")]
        public async Task<ActionResult<Employee>> Authorize(LoginCommand request)
        {
            var employee = context.Employee.GetByAuthData(request.Username, request.Password);
            if (employee != null)
                return employee;
            else
                return null;
        }
    }
}
