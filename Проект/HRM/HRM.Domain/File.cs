using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Domain
{
    public class File
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public int? PeriodId { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }
    }
}
