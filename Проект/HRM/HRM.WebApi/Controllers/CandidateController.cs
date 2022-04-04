using HRM.Domain;
using HRM.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly UnitOfWork context = new UnitOfWork(new HRMDBContext());

        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> GetAll()
        {
            var res = await context.Candidate.GetAllAsync();
            return Ok(res);
        }
        //[HttpGet]
        //public async Task<ActionResult<List<Candidate>>> GetInterviewed()
        //{
        //    var res = context.Candidate.GetInterviewed();
        //    return Ok(res);
        //}
    }
}
