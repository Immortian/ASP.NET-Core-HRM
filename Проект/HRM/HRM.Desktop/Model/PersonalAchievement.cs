using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class PersonalAchievement
    {
        public int AchievementId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodId { get; set; }
        public string Description { get; set; }
        public double Reward { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Period Period { get; set; }
    }
}
