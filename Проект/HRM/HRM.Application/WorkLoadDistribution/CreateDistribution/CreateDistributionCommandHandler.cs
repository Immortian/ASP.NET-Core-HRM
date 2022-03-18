using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution.CreateDistribution
{
    public class CreateDistributionCommandHandler
    {
        private IUnitOfWork unitOfWork;
        Period current;
        public CreateDistributionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            current = unitOfWork.Period.Next();
        }
        public async Task Distribute(CreateDistributionCommand request)
        {
            await unitOfWork.Period.CreateAsync(current);
            current.TotalWorkLoadHours = request.MonthlyHours; //добавить валидацию

            if (request.Options == null)
            {
                await StaticDistribute(request);
                return;
            }
            else
                await DinamicDistribute(request);
        }
        private async Task StaticDistribute(CreateDistributionCommand request)
        {
            int hoursPerEmployee = DistributedHours(request.MonthlyHours);
            foreach (var employee in unitOfWork.Employee.GetActive())
            {
                employee.EmployeeWorkLoads.Add(new EmployeeWorkLoad
                {
                    EmployeeId = employee.EmployeeId,
                    PeriodId = current.PeriodId,
                    WorkLoadHours = hoursPerEmployee,
                });
            }
            await unitOfWork.Save();
            foreach (var department in unitOfWork.Department.GetAllAsync().Result)
            {
                department.DepartmentWorkLoads.Add(new DepartmentWorkLoad
                {
                    DepartmentId = department.DepartmentId,
                    PeriodId = current.PeriodId,
                    WorkLoad = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHoursByDepartmentId(current.PeriodId,department.DepartmentId),
                });
            }
            await FinalSum();
            await unitOfWork.Save();
        }
        private async Task DinamicDistribute(CreateDistributionCommand request)
        {
            foreach (var option in request.Options)
            {
                if(option.StaticHours != 0)
                {
                    await DistributeWithStaticOption(option);
                }

            }
            await FinalSum();
            await unitOfWork.Save();
        }
        private int DistributedHours(int hours, int departmentId)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActiveByDepartmentId(departmentId).Count());
        }
        private int DistributedHours(int hours)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActive().Count());
        }
        private async Task DistributeWithStaticOption(DistributionOption option)
        {
            int hoursPerDepartmentEmployee = DistributedHours(option.StaticHours, option.DepartmentId);

            foreach (var employee in unitOfWork.Employee.GetActiveByDepartmentId(option.DepartmentId))
            {
                employee.EmployeeWorkLoads.Add(new EmployeeWorkLoad
                {
                    EmployeeId = employee.EmployeeId,
                    PeriodId = current.PeriodId,
                    WorkLoadHours = hoursPerDepartmentEmployee,
                });
            }
            await unitOfWork.Save();
            Department dep = unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result;
            var wl = new DepartmentWorkLoad
            {
                DepartmentId = dep.DepartmentId,
                PeriodId = current.PeriodId,
                WorkLoad = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHoursByDepartmentId(current.PeriodId, dep.DepartmentId),
            };
            dep.DepartmentWorkLoads.Add(wl);
            if (wl.WorkLoad != option.StaticHours) ;
            //messege
        }
        private async Task FinalSum()
        {
            current.TotalWorkLoadHours = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHours(current.PeriodId);
            await unitOfWork.Period.UpdateAsync(current);
        }
    }
}
