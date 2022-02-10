using CRM.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Candidates
{
    public class CandidateRepository : DbSet<Candidate>, ICandidateRepository
    {
        DbSet<Candidate> context;
        public CandidateRepository(DbSet<Candidate> _context)
        {
            context = _context;
        }
        public IEnumerable<Candidate> GetInterviewed()
        {
            return context.OrderBy(x => x.Passport.Surname).Where(x => x.Interviews.Any());
        }

        public IEnumerable<Candidate> GetNotInterviewed()
        {
            return context.OrderBy(x => x.Passport.Surname).Where(x => !(x.Interviews.Any()));
        }
    }
}
