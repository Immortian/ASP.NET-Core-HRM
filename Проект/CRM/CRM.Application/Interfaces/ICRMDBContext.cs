﻿using CRM.Domain;
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
        public CandidateRepository Candidates { get; set; }
        public ContactDataRepository ContactData { get; set; }
        public DepartmentRepository Departments { get; set; }
        public DepartmentWorkLoadRepository DepartmentWorkLoads { get; set; }
        public DbSet<Dismissal> Dismissals { get; set; }
        public DbSet<Document> Documents { get; set; }
        public EmployeeRepository Employees { get; set; }
        public EmployeeWorkLoadRepository EmployeeWorkLoads { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<PassportInfo> PassportInfos { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<PersonalAchievement> PersonalAchievements { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
