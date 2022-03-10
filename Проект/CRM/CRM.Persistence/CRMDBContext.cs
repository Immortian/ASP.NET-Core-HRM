using System;
using System.Collections.Generic;
using CRM.Application.Interfaces;
using CRM.Application.BusinessLogic.Candidates;
using CRM.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CRM.Application.BusinessLogic.ContactDatum;
using CRM.Application.BusinessLogic.Departments;
using CRM.Application.BusinessLogic.DepartmentWorkLoads;
using CRM.Application.BusinessLogic.Employees;
using CRM.Application.BusinessLogic.EmployeeWorkLoads;

namespace CRM.Persistence
{
    public partial class CRMDBContext : DbContext, ICRMDBContext
    {
        public CRMDBContext()
        {
        }

        public CRMDBContext(DbContextOptions<CRMDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorization> Authorizations { get; set; } = null!;
        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<ContactData> ContactData { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<DepartmentWorkLoad> DepartmentWorkLoads { get; set; } = null!;
        public virtual DbSet<Dismissal> Dismissals { get; set; } = null!;
        public virtual DbSet<Document> Documents { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeWorkLoad> EmployeeWorkLoads { get; set; } = null!;
        public virtual DbSet<Interview> Interviews { get; set; } = null!;
        public virtual DbSet<PassportInfo> PassportInfos { get; set; } = null!;
        public virtual DbSet<Period> Periods { get; set; } = null!;
        public virtual DbSet<PersonalAchievement> PersonalAchievements { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    //.UseLazyLoadingProxies() вместо этого modelBuilder.Entity<...>.Navigation("...").AutoInclude()
                    .UseSqlServer("Data Source=LAPTOP-5FUCQ052; Trusted_Connection=True; MultipleActiveResultSets=True; Initial Catalog=Call_centerTest");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorization>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("Authorization");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Authorizations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Authorization_Employee");

                entity.Navigation(x => x.Employee).AutoInclude();
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.HasIndex(e => e.ContactDataId, "UQ__Candidat__31660442B77D070C")
                    .IsUnique();

                entity.Property(e => e.CandidateId).HasColumnName("Candidate_id");

                entity.Property(e => e.ContactDataId).HasColumnName("Contact_data_id");

                entity.Property(e => e.Education).HasMaxLength(20);

                entity.Property(e => e.ExpirienseYears).HasColumnName("Expiriense_years");

                entity.Property(e => e.PassportId).HasColumnName("Passport_id");

                entity.HasOne(d => d.ContactData)
                    .WithOne(p => p.Candidate)
                    .HasForeignKey<Candidate>(d => d.ContactDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidate_Contact_data");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Candidate_Passport_info");

                entity.Navigation(x => x.ContactData).AutoInclude();
                entity.Navigation(x => x.Passport).AutoInclude();
            });

            modelBuilder.Entity<ContactData>(entity =>
            {
                entity.HasKey(e => e.ContactDataId);

                entity.ToTable("Contact_data");

                entity.Property(e => e.ContactDataId)
                    .ValueGeneratedNever()
                    .HasColumnName("Contact_data_id");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .HasColumnName("Phone_number");

                //entity.Navigation(x => x.Candidate).AutoInclude();
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_id");

                entity.Property(e => e.BasicMoneyPerHour).HasColumnName("basic_money_per_hour");

                entity.Property(e => e.Direction).HasMaxLength(50);

                entity.Property(e => e.EmployeeCount).HasColumnName("Employee_count");

                entity.Property(e => e.ManagerId).HasColumnName("Manager_id");

                entity.Property(e => e.TotalMoneyPerHour).HasColumnName("total_money_per_hour");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.ManagerId)
                    .HasConstraintName("FK_Department_Employee");

                entity.Navigation(x => x.Employees).AutoInclude();
            });

            modelBuilder.Entity<DepartmentWorkLoad>(entity =>
            {
                entity.HasKey(e => e.ScheduleId);

                entity.ToTable("Department_work_load");

                entity.Property(e => e.ScheduleId).HasColumnName("Schedule_id");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_id");

                entity.Property(e => e.IsEqualOrMore).HasColumnName("isEqualOrMore");

                entity.Property(e => e.PeriodId).HasColumnName("Period_id");

                entity.Property(e => e.WorkLoad).HasColumnName("Work_load");

                entity.Property(e => e.WorkedHours).HasColumnName("Worked_hours");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentWorkLoads)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_work_load_Department");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.DepartmentWorkLoads)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_Department_work_load_Period");

                entity.Navigation(x => x.Department).AutoInclude();
                entity.Navigation(x => x.Period).AutoInclude();
            });

            modelBuilder.Entity<Dismissal>(entity =>
            {
                entity.ToTable("Dismissal");

                entity.Property(e => e.DismissalId)
                    .ValueGeneratedNever()
                    .HasColumnName("Dismissal_id");

                entity.Property(e => e.DismissalDate)
                    .HasColumnType("date")
                    .HasColumnName("Dismissal_date");

                entity.Property(e => e.DismissalReason)
                    .HasMaxLength(50)
                    .HasColumnName("Dismissal_reason");

                entity.Property(e => e.DocumentDate)
                    .HasColumnType("date")
                    .HasColumnName("Document_date");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Dismissals)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Dismissal_Employee");

                //entity.Navigation(x => x.Passport).AutoInclude(); обновить модель
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.DocumentId)
                    .ValueGeneratedNever()
                    .HasColumnName("Document_id");

                entity.Property(e => e.CandidateId).HasColumnName("Candidate_id");

                entity.Property(e => e.DocumentType)
                    .HasMaxLength(50)
                    .HasColumnName("Document_type");

                entity.Property(e => e.DocumentUrl)
                    .HasMaxLength(50)
                    .HasColumnName("Document_url");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK_Document_Candidate");

                entity.Navigation(x => x.Candidate).AutoInclude();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.AuthorizationCode)
                    .HasMaxLength(50)
                    .HasColumnName("Authorization_code");

                entity.Property(e => e.ContactDataId).HasColumnName("Contact_data_id");

                entity.Property(e => e.DepartmentId).HasColumnName("Department_id");

                entity.Property(e => e.InterviewId).HasColumnName("Interview_id");

                entity.Property(e => e.PassportId).HasColumnName("Passport_id");

                entity.HasOne(d => d.ContactData)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.ContactDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Contact_data");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.InterviewId)
                    .HasConstraintName("FK_Employee_Interview");

                entity.HasOne(d => d.Passport)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PassportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_Passport_info");

                entity.Navigation(x => x.Authorizations).AutoInclude();
                entity.Navigation(x => x.ContactData).AutoInclude();
            });

            modelBuilder.Entity<EmployeeWorkLoad>(entity =>
            {
                entity.HasKey(e => e.AddendumId);

                entity.ToTable("Employee_work_load");

                entity.Property(e => e.AddendumId).HasColumnName("Addendum_id");

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.PeriodId).HasColumnName("Period_id");

                entity.Property(e => e.ResultSalary).HasColumnName("result_salary");

                entity.Property(e => e.WorkLoadHours).HasColumnName("Work_load_hours");

                entity.Property(e => e.WorkedHours).HasColumnName("Worked_hours");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeWorkLoads)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Employee_work_load_Employee");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.EmployeeWorkLoads)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_Employee_work_load_Period");

                entity.Navigation(x => x.Employee).AutoInclude();
                entity.Navigation(x => x.Period).AutoInclude();
            });

            modelBuilder.Entity<Interview>(entity =>
            {
                entity.ToTable("Interview");

                entity.Property(e => e.InterviewId).HasColumnName("Interview_id");

                entity.Property(e => e.CandidateId).HasColumnName("Candidate_id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Interviews)
                    .HasForeignKey(d => d.CandidateId)
                    .HasConstraintName("FK_Interview_Candidate");

                entity.Navigation(x => x.Candidate).AutoInclude();
            });

            modelBuilder.Entity<PassportInfo>(entity =>
            {
                entity.HasKey(e => e.PassportId);

                entity.ToTable("Passport_info");

                entity.Property(e => e.PassportId)
                    .ValueGeneratedNever()
                    .HasColumnName("Passport_id");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PassportNumber).HasColumnName("Passport_number");

                entity.Property(e => e.PassportSerial).HasColumnName("Passport_serial");

                entity.Property(e => e.State).HasMaxLength(50);

                entity.Property(e => e.Street).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("Period");

                entity.Property(e => e.PeriodId).HasColumnName("Period_id");

                entity.Property(e => e.TotalWorkLoadHours).HasColumnName("Total_work_load_hours");
            });

            modelBuilder.Entity<PersonalAchievement>(entity =>
            {
                entity.HasKey(e => e.AchievementId);

                entity.ToTable("Personal_achievements");

                entity.Property(e => e.AchievementId).HasColumnName("Achievement_id");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.EmployeeId).HasColumnName("Employee_id");

                entity.Property(e => e.PeriodId).HasColumnName("Period_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PersonalAchievements)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Personal_achievements_Employee");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.PersonalAchievements)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_Personal_achievements_Period");

                entity.Navigation(x => x.Period).AutoInclude();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
