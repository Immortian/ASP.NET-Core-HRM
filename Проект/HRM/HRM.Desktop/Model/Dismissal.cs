using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Dismissal
    {
        public int DismissalId { get; set; }
        public int PassportId { get; set; }
        public DateTime DocumentDate { get; set; }
        public DateTime DismissalDate { get; set; }
        public string DismissalReason { get; set; }
        public double Payments { get; set; }

        public virtual PassportInfo Passport { get; set; }
    }
}
