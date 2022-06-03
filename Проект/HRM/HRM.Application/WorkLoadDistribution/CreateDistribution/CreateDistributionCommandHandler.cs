using HRM.Application.Exceptions;
using HRM.Application.Interfaces;
using HRM.Application.WorkLoadDistribution.GenerateAddendum;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution.CreateDistribution
{
    public class CreateDistributionCommandHandler
    {
        private IUnitOfWork unitOfWork;
        private CommandValidation validation;
        private DistributionLogic logic;
        private GenerateAddendumCommandHandler handler;
        Period current;
        public CreateDistributionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            validation = new CommandValidation(unitOfWork);
            logic = new DistributionLogic(unitOfWork);
            handler = new GenerateAddendumCommandHandler(unitOfWork);
            current = unitOfWork.Period.Next();
        }
        public async Task Distribute(CreateDistributionCommand request)
        {
            await unitOfWork.Period.CreateAsync(current);
            current.TotalWorkLoadHours = request.MonthlyHours;

            if (request.Options == null)
            {
                if (validation.ValidateStatic(request.MonthlyHours, current))
                    await StaticDistribute(request);
                else
                    throw new ValidationException(nameof(CreateDistributionCommand));
            }
            else
            {
                if(validation.ValidateOptions(request.Options, current))
                    await DinamicDistribute(request);
                else
                    throw new ValidationException(nameof(CreateDistributionCommand));
            }
        }
        private async Task StaticDistribute(CreateDistributionCommand request)
        {
            int hoursPerEmployee = logic.DistributedHours(request.MonthlyHours);
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
            await logic.FinalSum(current);
            await unitOfWork.Save();
        }
        private async Task DinamicDistribute(CreateDistributionCommand request)
        {
            foreach (var option in request.Options)
            {
                int hoursPerDepartmentEmployee = logic.DistributedHours(option.StaticHours, option.DepartmentId);

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
                Department dep = await unitOfWork.Department.GetByIdAsync(option.DepartmentId);
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
            await logic.FinalSum(current);
            await unitOfWork.Save();
        }
    }
}
