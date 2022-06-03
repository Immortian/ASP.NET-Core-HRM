using HRM.Application.Interfaces;
using Quartz;

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
            var periodId = unitOfWork.Period.LastAsync().Result.PeriodId;
            foreach (var dep in unitOfWork.DepartmentWorkLoad.GetByPeriodId(periodId))
            {
                var moneyPerHour = dep.Department.TotalMoneyPerHour;
                foreach (var employee in unitOfWork.EmployeeWorkLoad.GetByPeriodIdPerDepartment(periodId, dep.DepartmentId))
                {
                    employee.ResultSalary = employee.WorkedHours <= employee.WorkLoadHours ? employee.WorkedHours : employee.WorkLoadHours;
                    employee.ResultSalary *= moneyPerHour;
                } 
            }
        }
    }
}
