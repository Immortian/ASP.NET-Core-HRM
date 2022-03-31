using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution
{
    public class CommandValidation
    {
        private IUnitOfWork unitOfWork;
        public CommandValidation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public bool ValidateOptions(List<DistributionOption> options, Period period)
        {
            if (options == null)
                return true;
            foreach (var option in options)
            {
                if (!ValidateOption(option, period))
                    return false;
            }
            return true;
        }
        private bool ValidateOption(DistributionOption option, Period period)
        {
            if (option.StaticHours >= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 4 * WorkDays(period) &&
                option.StaticHours <= unitOfWork.Department.GetByIdAsync(option.DepartmentId).Result.EmployeeCount * 10 * WorkDays(period))
                return true;
            else
                return false;
        }
        public bool ValidateStatic(int hours, Period period)
        {
            if (hours >= unitOfWork.Employee.GetActive().Count() * 4 * WorkDays(period) &&
                hours <= unitOfWork.Employee.GetActive().Count() * 10 * WorkDays(period))
                return true;
            else
                return false;
        }
        private int WorkDays(Period period)
        {
            int workDays = DateTime.DaysInMonth(period.Year, period.Month);
            for (int i = 1; i < DateTime.DaysInMonth(period.Year, period.Month) -1; i++)
            {
                DateTime dt = new DateTime(period.Year, period.Month, i);
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                    workDays--;
            }
            return workDays;
        }
    }
}
