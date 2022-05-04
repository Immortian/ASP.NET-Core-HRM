using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Dismissing
{
    public class DismissingCommand
    {
        public int EmployeeId { get; set; }
        public string Reason { get; set; }
        public double Payments { get; set; }
    }
}
