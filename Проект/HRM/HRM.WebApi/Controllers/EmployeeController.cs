using HRM.Application.Interfaces;
using HRM.Domain;
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
