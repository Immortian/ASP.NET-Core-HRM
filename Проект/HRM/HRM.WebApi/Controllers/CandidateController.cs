using HRM.Application.Interviewing;
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

        [HttpGet("all")]
        public async Task<ActionResult<List<Candidate>>> GetAll()
        {
            var res = await context.Candidate.GetAllAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> TakeInterview(InterviewingCommand request)
        {
            InterviewingCommandHandler intrvw = new InterviewingCommandHandler(context);
            await intrvw.TakeInterview(request);
            return Ok(request);
        }
        [HttpGet("interviewed")]
        public async Task<ActionResult<List<Candidate>>> GetInterviewed()
        {
            var res = context.Candidate.GetInterviewed();
            return Ok(res);
        }
    }
}
