using System.Collections.Generic;

namespace HRM.Desktop.Commands
{
    public class DistributionCommand
    {
        public int MonthlyHours { get; set; }
        public List<DistributionOption> Options { get; set; }
    }
}
