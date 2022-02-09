using System;
using System.Collections.Generic;

namespace CRM.Domain.Model
{
    public partial class Dismissal
    {
        public int DismissalId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DismissalDate { get; set; }
        public string DismissalReason { get; set; } = null!;
        public double Payments { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
