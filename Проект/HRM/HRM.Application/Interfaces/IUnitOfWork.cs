using HRM.Application.BuisnessLogic;

namespace HRM.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorizationRepository Authorization { get; set; }
         ICandidateRepository Candidate { get; set; }
         IContactDataRepository ContactData { get; set; }
         IDepartmentRepository Department { get; set; }
         IDepartmentWorkLoadRepository DepartmentWorkLoad { get; set; }
         IEmployeeRepository Employee { get; set; }
         IEmployeeWorkLoadRepository EmployeeWorkLoad { get; set; }
         IPeriodRepository Period { get; set; }
         Task Save();
    }
}
