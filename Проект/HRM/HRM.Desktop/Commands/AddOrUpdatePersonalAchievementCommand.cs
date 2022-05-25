using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Desktop.Commands
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
