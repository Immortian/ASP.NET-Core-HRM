using HRM.Application.Interfaces;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Salary
{
    public class MonthResultSalaryHandler : IJob
    {
        private readonly IUnitOfWork unitOfWork;

        public MonthResultSalaryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            foreach (var dep in unitOfWork.DepartmentWorkLoad.GetByPeriodId(unitOfWork.Period.LastAsync().Result.PeriodId))
            {
                var moneyPerHour = dep.Department.TotalMoneyPerHour;
                foreach (var employee in unitOfWork.EmployeeWorkLoad.GetByPeriodIdPerDepartment(unitOfWork.Period.LastAsync().Result.PeriodId, dep.DepartmentId))
                {
                    employee.ResultSalary = employee.WorkedHours <= employee.WorkLoadHours ? employee.WorkedHours : employee.WorkLoadHours;
                    employee.ResultSalary *= moneyPerHour;
                } 
            }
        }
    }
}
