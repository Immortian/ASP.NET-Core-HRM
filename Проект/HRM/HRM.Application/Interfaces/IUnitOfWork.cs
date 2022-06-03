using HRM.Application.BuisnessLogic;

namespace HRM.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IAuthorizationRepository Authorization { get; set; }
        IAchievementRepository Achievement { get; set; }
        ICandidateRepository Candidate { get; set; }
        ICompanyDataRepository CompanyData { get; set; }
        IContactDataRepository ContactData { get; set; }
        IDepartmentRepository Department { get; set; }
        IDepartmentWorkLoadRepository DepartmentWorkLoad { get; set; }
        IEmployeeRepository Employee { get; set; }
        IEmployeeWorkLoadRepository EmployeeWorkLoad { get; set; }
        IFileRepository File { get; set; }
        IDismissalRepository Dismissal { get; set; }
        IInterviewRepository Interview { get; set; }
        IPeriodRepository Period { get; set; }
        Task Save();
    }
}
