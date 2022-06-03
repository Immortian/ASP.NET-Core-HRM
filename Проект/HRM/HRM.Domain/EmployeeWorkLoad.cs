namespace HRM.Domain
{
    public partial class EmployeeWorkLoad
    {
        public int AddendumId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodId { get; set; }
        public int WorkLoadHours { get; set; }
        public int? WorkedHours { get; set; }
        public double? ResultSalary { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}
