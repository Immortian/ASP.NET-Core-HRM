using HRM.Application.Exeptions;
using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution.UpdateDistribution
{
    public class UpdateDistributionCommandHandler
    {
        private IUnitOfWork unitOfWork;
        private CommandValidation validation;
        private DistributionLogic logic;
        private Period current;
        public UpdateDistributionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            validation = new CommandValidation(unitOfWork);
            logic = new DistributionLogic(unitOfWork);
        }

        public async Task UpdateDistribution(UpdateDistributionCommand request)
        {
            current = unitOfWork.Period.GetByIdAsync(request.PeriodId).Result;
            if (current == null)
                throw new Exception(); // not found exception

            if (request.Options == null)
            {
                if (validation.ValidateStatic(request.MonthlyHours, current))
                    await StaticUpdate(request);
                else
                    throw new ValidationException(nameof(UpdateDistributionCommand));
            }
            else
            {
                if(validation.ValidateOptions(request.Options, current))
                    await DinamicUpdate(request);
                else
                    throw new ValidationException(nameof(UpdateDistributionCommand));
            }
        }

        private async Task DinamicUpdate(UpdateDistributionCommand request)
        {
            foreach (var option in request.Options)
            {
                int hoursPerDepartmentEmployee = logic.DistributedHours(option.StaticHours, option.DepartmentId);

                foreach (var workload in unitOfWork.EmployeeWorkLoad.GetByPeriodIdPerDepartment(request.PeriodId, option.DepartmentId))
                {
                    if (workload.WorkedHours != null && hoursPerDepartmentEmployee <= workload.WorkedHours)
                        workload.WorkLoadHours = (int)workload.WorkedHours;
                    else
                        workload.WorkLoadHours = hoursPerDepartmentEmployee;

                    await unitOfWork.EmployeeWorkLoad.UpdateAsync(workload);
                }
                foreach (var employee in unitOfWork.Employee.GetActiveWithNoWorkLoadByPeriodIdPerDepartment(request.PeriodId, option.DepartmentId)) //добавление в план новых сотрудников
                {
                    employee.EmployeeWorkLoads.Add(new EmployeeWorkLoad
                    {
                        EmployeeId = employee.EmployeeId,
                        PeriodId = current.PeriodId,
                        WorkLoadHours = hoursPerDepartmentEmployee,
                    });
                }
                await unitOfWork.Save();

                var schedule = unitOfWork.DepartmentWorkLoad.GetByPeriodIdPerDepartment(request.PeriodId, option.DepartmentId);
                schedule.WorkLoad = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHoursByDepartmentId(current.PeriodId, schedule.DepartmentId);
                await unitOfWork.DepartmentWorkLoad.UpdateAsync(schedule);
                if (schedule.WorkLoad != option.StaticHours) ;
                    //messege округлено
            }
            await logic.FinalSum(current);
            await unitOfWork.Save();
        }

        private async Task StaticUpdate(UpdateDistributionCommand request)
        {
            int hoursPerEmployee = logic.DistributedHours(request.MonthlyHours);
            foreach(var workload in unitOfWork.EmployeeWorkLoad.GetByPeriodId(request.PeriodId))
            {
                if (workload.WorkedHours != null && hoursPerEmployee <= workload.WorkedHours)
                    workload.WorkLoadHours = (int)workload.WorkedHours;
                else
                    workload.WorkLoadHours = hoursPerEmployee;

                await unitOfWork.EmployeeWorkLoad.UpdateAsync(workload);
            }
            foreach (var employee in unitOfWork.Employee.GetActiveWithNoWorkLoadByPeriodId(request.PeriodId)) //добавление в план новых сотрудников
            {
                employee.EmployeeWorkLoads.Add(new EmployeeWorkLoad
                {
                    EmployeeId = employee.EmployeeId,
                    PeriodId = current.PeriodId,
                    WorkLoadHours = hoursPerEmployee,
                });
            }
            await unitOfWork.Save();
            foreach (var departmentWorkLoad in unitOfWork.DepartmentWorkLoad.GetByPeriodId(request.PeriodId))
            {
                departmentWorkLoad.WorkLoad = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHoursByDepartmentId(current.PeriodId, departmentWorkLoad.DepartmentId);
                await unitOfWork.DepartmentWorkLoad.UpdateAsync(departmentWorkLoad);
            }
            await logic.FinalSum(current);
            await unitOfWork.Save();
        }
    }
}
