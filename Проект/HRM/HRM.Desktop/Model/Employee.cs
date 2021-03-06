using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Employee
    {
        public Employee()
        {
            Authorizations = new HashSet<Authorization>();
            Departments = new HashSet<Department>();
            EmployeeWorkLoads = new HashSet<EmployeeWorkLoad>();
            PersonalAchievements = new HashSet<PersonalAchievement>();
        }

        public int EmployeeId { get; set; }
        public int PassportId { get; set; }
        public int? DepartmentId { get; set; }
        public int ContactDataId { get; set; }
        public int InterviewId { get; set; }
        public bool Active { get; set; }
        public string AuthorizationCode { get; set; }

        public virtual ContactData ContactData { get; set; }
        public virtual Department Department { get; set; }
        public virtual Interview Interview { get; set; }
        public virtual PassportInfo Passport { get; set; }
        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<EmployeeWorkLoad> EmployeeWorkLoads { get; set; }
        public virtual ICollection<PersonalAchievement> PersonalAchievements { get; set; }
    }
}
