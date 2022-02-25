using System;
using System.Collections.Generic;
using System.Data.Entity;
using CRM.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Candidates
{
    public interface ICandidateRepository
    {
        IEnumerable<Candidate> GetNotInterviewed();
        IEnumerable<Candidate> GetInterviewed();
    }
}
