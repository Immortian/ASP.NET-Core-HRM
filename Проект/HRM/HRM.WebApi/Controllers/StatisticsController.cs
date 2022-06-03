using HRM.Application.Interfaces;
using HRM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private IUnitOfWork context { get; }

        public StatisticsController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpGet("Period")]
        public async Task<List<Period>> GetPeriods()
        {
            return await context.Period.GetAllAsync();
        }

        [HttpGet("Period/Current")]
        public Period? GetCurrentPeriod()
        {
            return context.Period.GetPeriodByDate(DateTime.Now);
        }
        [HttpGet("Period/Next")]
        public Period? GetNextPeriod()
        {
            return context.Period.GetPeriodByDate(DateTime.Now.AddMonths(1));
        }
        [HttpGet("Period/{Id}")]
        public async Task<Period> GetPeriodById(int Id)
        {
            return await context.Period.GetByIdAsync(Id);
        }
        [HttpGet("Period/Previous")]
        public async Task<ActionResult<Period?>> GetPreviousPeriod()
        {
            var period = context.Period.GetPeriodByDate(DateTime.Now.AddMonths(-1));
            if (period != null)
                return Ok(period);
            else
                return Ok(null);
        }
        [HttpGet("Department")]
        public async Task<List<Department>> GetDepartments()
        {
            return await context.Department.GetAllAsync();
        }

        [HttpGet("WorkLoad/Department/{Id}")]
        public List<DepartmentWorkLoad> GetDepartmentWorkLoadsOfPeriod(int Id)
        {
            return context.DepartmentWorkLoad.GetByPeriodId(Id).ToList();
        }
        [HttpGet("WorkLoad/Employee/{Id}")]
        public List<EmployeeWorkLoad> GetEmployeeWorkLoadsOfPeriod(int Id)
        {
            return context.EmployeeWorkLoad.GetByPeriodId(Id).ToList();
        }
    }
}
