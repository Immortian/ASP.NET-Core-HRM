using System;
using System.Collections.Generic;

namespace HRM.Domain
{
    public partial class Dismissal
    {
        public int DismissalId { get; set; }
        public int PassportId { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DismissalDate { get; set; }
        public string DismissalReason { get; set; } = null!;
        public double Payments { get; set; }

        public virtual PassportInfo Passport { get; set; } = null!;
    }
}
