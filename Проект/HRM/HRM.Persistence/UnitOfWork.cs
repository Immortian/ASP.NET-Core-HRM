using HRM.Application.BuisnessLogic;
using HRM.Application.Interfaces;
using HRM.Persistence.Repositories;

namespace HRM.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HRMDBContext context;
        public UnitOfWork(HRMDBContext _context)
        {
            context = _context;
            Authorization = new AuthorizationRepository(context);
            Achievement = new AchievementRepository(context);
            Candidate = new CandidateRepository(context);
            CompanyData = new CompanyDataRepository(context);
            ContactData = new ContactDataRepository(context);
            Department = new DepartmentRepository(context);
            DepartmentWorkLoad = new DepartmentWorkLoadRepository(context);
            File = new FileRepository(context);
            Employee = new EmployeeRepository(context);
            EmployeeWorkLoad = new EmployeeWorkLoadRepository(context);
            Period = new PeriodRepository(context);
            Interview = new InterviewRepository(context);
            Dismissal = new DismissalRepository(context);
        }

        public IAchievementRepository Achievement { get; set; }
        public ICompanyDataRepository CompanyData { get; set; }
        public ICandidateRepository Candidate { get ; set; }
        public IContactDataRepository ContactData { get; set; }
        public IDepartmentRepository Department { get; set; }
        public IDepartmentWorkLoadRepository DepartmentWorkLoad { get; set; }
        public IEmployeeRepository Employee { get; set; }
        public IEmployeeWorkLoadRepository EmployeeWorkLoad { get; set; }
        public IDismissalRepository Dismissal{ get; set; }
        public IFileRepository File { get; set; }
        public IPeriodRepository Period { get; set; }
        public IAuthorizationRepository Authorization { get; set; }
        public IInterviewRepository Interview { get; set; }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
