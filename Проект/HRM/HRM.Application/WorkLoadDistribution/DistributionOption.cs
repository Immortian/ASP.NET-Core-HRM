namespace HRM.Application.WorkLoadDistribution
{
    public class DistributionOption
    {
        public int DepartmentId { get; set; }
        //public int MinHours { get; set; } нет необходимости
        //public int MaxHours { get; set; }
        public int StaticHours { get; set; }
    }
}
