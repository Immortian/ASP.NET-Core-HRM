using CRM.Domain;
using CRM.Persistence;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly UnitOfWork context = new UnitOfWork(new CRMDBContext());

        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> GetAll()
        {
            var res = await context.Candidate.GetByIdAsync(1);
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
