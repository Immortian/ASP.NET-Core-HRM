using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
using HRM.Application.WorkLoadDistribution.ReadDistribution;
using HRM.Application.WorkLoadDistribution.UpdateDistribution;
using HRM.Domain;
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
            var handler = new CreateDistributionCommandHandler(context);
            await handler.Distribute(request);
            return Ok(request);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDistribution(UpdateDistributionCommand request)
        {
            var handler = new UpdateDistributionCommandHandler(context);
            await handler.UpdateDistribution(request);
            return Ok(request);
        }
        [HttpGet("Employee")]
        public async Task<ActionResult<EmployeeWorkLoad>> CheckEmployeeStatistics()
        {
            var handler = new ReadDistributionCommandHandler(context);
            var res = await handler.ReadEmployeeWorkLoads(DateTime.Now);
            return Ok(res);
        }
        [HttpGet("Department")]
        public async Task<ActionResult<DepartmentWorkLoad>> CheckDepartmentStatistics()
        {
            var handler = new ReadDistributionCommandHandler(context);
            var res = await handler.ReadDepartmentWorkLoads(DateTime.Now);
            return Ok(res);
        }
    }
}
