using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.WorkLoadDistribution.GenerateAddendum
{
    public class GenerateAddendumCommand
    {
        public int EmployeeId { get; set; }
        public int WorkLoad { get; set; }
        public int PeriodId { get; set; }
    }
}
