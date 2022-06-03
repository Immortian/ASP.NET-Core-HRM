namespace HRM.Application.AchievementsHandling
{
    public class AddOrUpdatePersonalAchievementCommand
    {
        public int AchievementId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodId { get; set; }
        public string Description { get; set; }
        public double Reward { get; set; }
    }
}
