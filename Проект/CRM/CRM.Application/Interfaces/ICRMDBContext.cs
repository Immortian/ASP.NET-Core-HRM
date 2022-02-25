using CRM.Domain;
using Microsoft.EntityFrameworkCore;
using CRM.Application.BusinessLogic.Candidates;
using CRM.Application.BusinessLogic.ContactDatum;
using CRM.Application.BusinessLogic.Departments;
using CRM.Application.BusinessLogic.DepartmentWorkLoads;
using CRM.Application.BusinessLogic.Employees;
using CRM.Application.BusinessLogic.EmployeeWorkLoads;

namespace CRM.Application.Interfaces
{
    public interface ICRMDBContext 
    {
        public DbSet<Authorization> Authorizations { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<ContactData> ContactData { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentWorkLoad> DepartmentWorkLoads { get; set; }
        public DbSet<Dismissal> Dismissals { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeWorkLoad> EmployeeWorkLoads { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<PassportInfo> PassportInfos { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PersonalAchievement> PersonalAchievements { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
