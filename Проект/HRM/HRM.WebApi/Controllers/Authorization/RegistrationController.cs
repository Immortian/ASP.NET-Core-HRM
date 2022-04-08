using HRM.Application.Authorization.Registration;
using HRM.Application.Interfaces;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers.Authorization
{
    [ApiController]
    [Route("api/authorization/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork context = new UnitOfWork(new HRMDBContext());

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationCommand request)
        {
            RegistrationCommandHandler reg = new RegistrationCommandHandler(context);
            await reg.Registration(request);
            return Ok(request);
        }
    }
}
