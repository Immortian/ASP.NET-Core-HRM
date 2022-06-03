using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution
{
    public class DistributionLogic
    {
        private IUnitOfWork unitOfWork;
        public DistributionLogic(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public int DistributedHours(int hours, int departmentId)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActiveByDepartmentId(departmentId).Count());
        }
        public int DistributedHours(int hours)
        {
            return (int)Math.Round((double)hours / unitOfWork.Employee.GetActive().Count());
        }
        public async Task FinalSum(Period current)
        {
            current.TotalWorkLoadHours = unitOfWork.EmployeeWorkLoad.SumPeriodWorkLoadHours(current.PeriodId);
            await unitOfWork.Period.UpdateAsync(current);
        }
    }
}
