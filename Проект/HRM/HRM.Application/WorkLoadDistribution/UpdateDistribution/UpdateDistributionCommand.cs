using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.WorkLoadDistribution.UpdateDistribution
{
    public class UpdateDistributionCommand : DistributionCommand
    {
        public int PeriodId { get; set; }
    }
}
