using HRM.Application.CompanyData;
using HRM.Application.Interfaces;
using HRM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyDataController : Controller
    {
        private readonly IUnitOfWork context;
        public CompanyDataController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> FillCompanyData(FillCompanyDataCommand request)
        {
            var handler = new FillCompanyDataCommandHandler(context);
            await handler.AddOrUpdate(request);
            return Ok(request);
        }
        [HttpGet]
        public async Task<ActionResult<CompanyData>> CheckCompanyData()
        {
            return await context.CompanyData.FirstAsync();
        }
    }
}
