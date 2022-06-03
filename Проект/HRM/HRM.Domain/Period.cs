namespace HRM.Domain
{
    public partial class Period
    {
        public Period()
        {
            DepartmentWorkLoads = new HashSet<DepartmentWorkLoad>();
            EmployeeWorkLoads = new HashSet<EmployeeWorkLoad>();
            PersonalAchievements = new HashSet<PersonalAchievement>();
        }

        public int PeriodId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalWorkLoadHours { get; set; }

        public virtual ICollection<DepartmentWorkLoad> DepartmentWorkLoads { get; set; }
        public virtual ICollection<EmployeeWorkLoad> EmployeeWorkLoads { get; set; }
        public virtual ICollection<PersonalAchievement> PersonalAchievements { get; set; }
    }
}
