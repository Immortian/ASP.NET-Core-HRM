using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.WorkLoadDistribution.ReadDistribution
{
    public class ReadDistributionCommandHandler
    {
        private IUnitOfWork unitOfWork;
        Period current;
        public ReadDistributionCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<EmployeeWorkLoad>> ReadEmployeeWorkLoads(DateTime request)
        {
            current = unitOfWork.Period.GetPeriodByDate(request);
            var Workloads = unitOfWork.EmployeeWorkLoad.GetByPeriodId(current.PeriodId);
            return Workloads.ToList();
        }
        public async Task<List<DepartmentWorkLoad>> ReadDepartmentWorkLoads(DateTime request)
        {
            current = unitOfWork.Period.GetPeriodByDate(request);
            var Workloads = unitOfWork.DepartmentWorkLoad.GetByPeriodId(current.PeriodId);
            return Workloads.ToList();
        }
    }
}
