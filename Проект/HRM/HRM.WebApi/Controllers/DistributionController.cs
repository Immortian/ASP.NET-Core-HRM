using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
using HRM.Application.WorkLoadDistribution.UpdateDistribution;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

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
            var distribution = new CreateDistributionCommandHandler(context);
            await distribution.Distribute(request);
            return Ok(request);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDistribution(UpdateDistributionCommand request)
        {
            var distribution = new UpdateDistributionCommandHandler(context);
            await distribution.UpdateDistribution(request);
            return Ok(request);
        }
    }
}
