using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public int CandidateId { get; set; }
        public string DocumentType { get; set; }
        public string DocumentUrl { get; set; }

        public virtual Candidate Candidate { get; set; }
    }
}
