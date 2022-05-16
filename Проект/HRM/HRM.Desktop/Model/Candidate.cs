using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Candidate
    {
        public Candidate()
        {
            Documents = new HashSet<Document>();
            Interviews = new HashSet<Interview>();
        }

        public int CandidateId { get; set; }
        public int PassportId { get; set; }
        public int ContactDataId { get; set; }
        public string Education { get; set; }
        public int ExpirienseYears { get; set; }

        public virtual ContactData ContactData { get; set; }
        public virtual PassportInfo Passport { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Interview> Interviews { get; set; }
    }
}
