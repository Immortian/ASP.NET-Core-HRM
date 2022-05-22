using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class PassportInfo
    {
        public PassportInfo()
        {
            Candidates = new HashSet<Candidate>();
            Employees = new HashSet<Employee>();
            Dismissals = new HashSet<Dismissal>();
        }

        public int PassportId { get; set; }
        public int PassportSerial { get; set; }
        public int PassportNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House { get; set; }
        public int? Buinding { get; set; }
        public int Apartment { get; set; }

        public string Inits { get { return Surname + " " + Name[0] + "." + Lastname[0] + "."; } }

        public virtual ICollection<Candidate> Candidates { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Dismissal> Dismissals { get; set; }
    }
}
