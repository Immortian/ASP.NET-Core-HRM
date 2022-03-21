using HRM.Application.Exeptions;
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
            if (!ValidateStatic(request.MonthlyHours, current) || !ValidateOptions(request.Options, current))
                throw new ValidationExeption(nameof(CreateDistributionCommand));
                
            await unitOfWork.Period.CreateAsync(current);
            current.TotalWorkLoadHours = request.MonthlyHours;

            if (request.Options == null)
            {
                await StaticDistribute(request);
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
                //messege округлено
            }
            await FinalSum();
            await unitOfWork.Save();
        }
        private bool ValidateOptions(List<DistributionOption> options, Period period)
        {
            if(options == null)
                return true;
            foreach (var option in options)
            {
                if (!ValidateOption(option, current))
                    return false;
            }
            return true;
        }
        private bool ValidateOption(DistributionOption option, Period period)
        {
            if (period.Month != 2)
            {
                if (option.StaticHours >= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 4 * 21 &&
                    option.StaticHours <= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 10 * 25)
                    return true;
                else
                    return false;
            }
            else
            {
                if (option.StaticHours >= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 4 * 20 &&
                    option.StaticHours <= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 10 * 20)
                    return true;
                else
                    return false;
            }
        }
        private bool ValidateStatic(int hours, Period period)
        {
            if (period.Month != 2)
            {
                if (hours >= unitOfWork.Employee.GetActive().Count() * 4 * 21 &&
                    hours <= unitOfWork.Employee.GetActive().Count() * 10 * 25)
                    return true;
                else
                    return false;
            }
            else
            {
                if (hours >= unitOfWork.Employee.GetActive().Count() * 4 * 20 &&
                    hours <= unitOfWork.Employee.GetActive().Count() * 10 * 20)
                    return true;
                else
                    return false;
            }
        }
        private int DistributedHours(int hours, int departmentId)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActiveByDepartmentId(departmentId).Count());
        }
        private int DistributedHours(int hours)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActive().Count());
        }
        private async Task FinalSum()
        {
            current.TotalWorkLoadHours = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHours(current.PeriodId);
            await unitOfWork.Period.UpdateAsync(current);
        }
    }
}
