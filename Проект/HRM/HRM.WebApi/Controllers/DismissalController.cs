using HRM.Application.Dismissing;
using HRM.Application.Interfaces;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DismissalController : ControllerBase
    {
        private readonly IUnitOfWork context;
        public DismissalController(IUnitOfWork context)
        {
            this.context = context;
        }
        [HttpDelete]
        public async Task<IActionResult> DismissEmployee(DismissingCommand request)
        {
            var handler = new DismissingCommandHandler(context);
            await handler.Dismiss(request);
            return Ok(request);
        }
    }
}
