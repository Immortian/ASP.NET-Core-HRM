using System;
using System.Collections.Generic;

namespace CRM.Domain
{
    public partial class EmployeeWorkLoad
    {
        public int AddendumId { get; set; }
        public int EmployeeId { get; set; }
        public int PeriodId { get; set; }
        public double WorkLoadHours { get; set; }
        public double? WorkedHours { get; set; }
        public double? ResultSalary { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}
