using System;
using System.Collections.Generic;

namespace CRM.Domain.Model
{
    public partial class PersonalAchievement
    {
        public int AchievementId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodId { get; set; }
        public string Description { get; set; } = null!;
        public double Reward { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}
