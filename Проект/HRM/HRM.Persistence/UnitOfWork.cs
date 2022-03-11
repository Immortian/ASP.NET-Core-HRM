using HRM.Persistence.Repositories;

namespace HRM.Persistence
{
    public class UnitOfWork
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
        }
        public CandidateRepository Candidate { get; set; }
        public ContactDataRepository ContactData { get; set; }
        public DepartmentRepository Department { get; set; }
        public DepartmentWorkLoadRepository DepartmentWorkLoad { get; set; }
        public EmployeeRepository Employee { get; set; }
        public EmployeeWorkLoadRepository EmployeeWorkLoad { get; set; }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
