using System;
using System.Collections.Generic;

namespace CRM.Domain.Model
{
    public partial class DepartmentWorkLoad
    {
        public int ScheduleId { get; set; }
        public int DepartmentId { get; set; }
        public int PeriodId { get; set; }
        public double WorkLoad { get; set; }
        public double? WorkedHours { get; set; }
        public bool? IsEqualOrMore { get; set; }

        public virtual Department Department { get; set; } = null!;
        public virtual Period Period { get; set; } = null!;
    }
}
