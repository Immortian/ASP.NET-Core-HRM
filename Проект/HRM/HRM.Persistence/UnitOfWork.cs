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
            Candidate = new CandidateRepository(context);
            ContactData = new ContactDataRepository(context);
            Department = new DepartmentRepository(context);
            DepartmentWorkLoad = new DepartmentWorkLoadRepository(context);
            Employee = new EmployeeRepository(context);
            EmployeeWorkLoad = new EmployeeWorkLoadRepository(context);
            Period = new PeriodRepository(context);
        }

        public ICandidateRepository Candidate { get ; set; }
        public IContactDataRepository ContactData { get; set; }
        public IDepartmentRepository Department { get; set; }
        public IDepartmentWorkLoadRepository DepartmentWorkLoad { get; set; }
        public IEmployeeRepository Employee { get; set; }
        public IEmployeeWorkLoadRepository EmployeeWorkLoad { get; set; }
        public IPeriodRepository Period { get; set; }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
