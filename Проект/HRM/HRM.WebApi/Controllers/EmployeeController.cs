using HRM.Application.Interfaces;
using HRM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace HRM.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork context;

        public EmployeeController(IUnitOfWork context)
        {
            this.context = context;
        }

        [HttpGet]
        public List<Employee> GetActiveEmployee()
        {
            return context.Employee.GetActive().ToList();
        }
    }
}
