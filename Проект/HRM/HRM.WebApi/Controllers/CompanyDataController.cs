using HRM.Application.CompanyData;
using HRM.Application.Interfaces;
using HRM.Domain;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ActionResult<CompanyData>> CheckCompanyData(FillCompanyDataCommand request)
        {
            return await context.CompanyData.FirstAsync();
        }
    }
}
