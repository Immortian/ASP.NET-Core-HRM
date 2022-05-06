using HRM.Domain;
using Microsoft.EntityFrameworkCore;
using File = HRM.Domain.File;

namespace HRM.Application.Interfaces
{
    public interface IHRMDBContext 
    {
        public DbSet<Domain.Authorization> Authorizations { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Domain.CompanyData> CompanyData { get; set; }
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
        public DbSet<File> Files { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
