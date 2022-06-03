namespace HRM.Domain
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
        public string AuthorizationCode { get; set; } = null!;

        public virtual ContactData ContactData { get; set; } = null!;
        public virtual Department? Department { get; set; }
        public virtual Interview Interview { get; set; } = null!;
        public virtual PassportInfo Passport { get; set; } = null!;
        public virtual ICollection<Authorization> Authorizations { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<EmployeeWorkLoad> EmployeeWorkLoads { get; set; }
        public virtual ICollection<PersonalAchievement> PersonalAchievements { get; set; }
    }
}
