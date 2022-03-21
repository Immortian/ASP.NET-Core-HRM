namespace HRM.Application.WorkLoadDistribution.CreateDistribution
{
    public class CreateDistributionCommand
    {
        public int MonthlyHours { get; set; }
        public List<DistributionOption>? Options { get; set; }
    }
}
