using CRM.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Persistence
{
    public class UnitOfWork
    {
        private readonly CRMContext context;
        public UnitOfWork(CRMContext _context)
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
