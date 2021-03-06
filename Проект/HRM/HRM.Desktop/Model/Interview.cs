using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Interview
    {
        public Interview()
        {
            Employees = new HashSet<Employee>();
        }

        public int InterviewId { get; set; }
        public int CandidateId { get; set; }
        public bool IsPassed { get; set; }
        public DateTime Date { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
