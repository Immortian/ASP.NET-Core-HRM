using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
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

    public class DistributionController : Controller
    {
        private readonly IUnitOfWork context = new UnitOfWork(new HRMDBContext());
        [HttpPost]
        public async Task<IActionResult> Distribute(CreateDistributionCommand request)
        {
            CreateDistributionCommandHandler distribution = new CreateDistributionCommandHandler(context);
            await distribution.Distribute(request);
            return Ok(request);
        }
    }
}
