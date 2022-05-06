using HRM.Application.Interfaces;
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
        private readonly IUnitOfWork context;
        public CandidateController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Candidate>>> GetAll()
        {
            var res = await context.Candidate.GetAllAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> TakeInterview(InterviewingCommand request)
        {
            InterviewingCommandHandler handler = new InterviewingCommandHandler(context);
            await handler.TakeInterview(request);
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
