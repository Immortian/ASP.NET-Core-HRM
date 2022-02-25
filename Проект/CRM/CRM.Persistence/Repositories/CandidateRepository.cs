using CRM.Application.BusinessLogic.Candidates;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using System.Data.Entity;

namespace CRM.Persistence.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(CRMContext context) : base(context)
        {
        }

        public IEnumerable<Candidate> GetInterviewed()
        {
            return context.Candidates.OrderBy(x => x.Passport.Surname).Where(x => x.Interviews.Any()).AsNoTracking();
        }

        public IEnumerable<Candidate> GetNotInterviewed()
        {
            return context.Candidates.OrderBy(x => x.Passport.Surname).Where(x => !(x.Interviews.Any()));
        }
    }
}
