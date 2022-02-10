using System;
using System.Collections.Generic;

namespace CRM.Domain
{
    public partial class ContactDatum
    {
        public ContactDatum()
        {
            Employees = new HashSet<Employee>();
        }

        public int ContactDataId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual Candidate Candidate { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
