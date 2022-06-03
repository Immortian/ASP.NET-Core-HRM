namespace HRM.Application.WorkLoadDistribution
{
    public class DistributionCommand
    {
        public int MonthlyHours { get; set; }
        public List<DistributionOption>? Options { get; set; }
    }
}
