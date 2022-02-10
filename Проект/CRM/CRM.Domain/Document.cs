using System;
using System.Collections.Generic;

namespace CRM.Domain
{
    public partial class Document
    {
        public int DocumentId { get; set; }
        public int CandidateId { get; set; }
        public string DocumentType { get; set; } = null!;
        public string DocumentUrl { get; set; } = null!;

        public virtual Candidate Candidate { get; set; } = null!;
    }
}
