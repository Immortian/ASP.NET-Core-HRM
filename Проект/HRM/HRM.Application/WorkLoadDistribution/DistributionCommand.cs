using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.WorkLoadDistribution
{
    public class DistributionCommand
    {
        public int MonthlyHours { get; set; }
        public List<DistributionOption>? Options { get; set; }
    }
}
