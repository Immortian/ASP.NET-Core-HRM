using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class DepartmentWorkLoad
    {
        public int ScheduleId { get; set; }
        public int DepartmentId { get; set; }
        public int PeriodId { get; set; }
        public int WorkLoad { get; set; }
        public int? WorkedHours { get; set; }
        public bool? IsEqualOrMore { get; set; }

        public virtual Department Department { get; set; }
        public virtual Period Period { get; set; }
    }
}
