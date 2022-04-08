using HRM.Application.Authorization.Login;
using HRM.Application.Interfaces;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers.Authorization
{
    [ApiController]
    [Route("api/authorization/[controller]")]
    public class LoginController : Controller
    {
        private readonly IUnitOfWork context = new UnitOfWork(new HRMDBContext());

        //[HttpGet]
        //public IActionResult Login(string returnUrl)
        //{
        //    var viewModel = new LoginViewModel
        //    {
        //        ReturnUrl = returnUrl
        //    };
        //    return View(viewModel);
        //}

        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginCommand request)
        {
            LoginCommandHandler reg = new LoginCommandHandler(context);
            int permission = await reg.TryLogin(request);
            if (permission == 3)
                return ""; //returnUrl to admin home page
            if(permission == 2)
                return ""; //returnUrl to admin home page
            if(permission == 1)
                return ""; //returnUrl to admin home page
            return Ok(request);
        }
    }
}
