using CRM.Application.BusinessLogic.Candidates;
using CRM.Domain;
using CRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CRM.Persistence.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(CRMDBContext context) : base(context)
        {
        }
        public async Task<List<Candidate>> GetInterviewed()
        {
            return await context.Candidates
                .OrderBy(x => x.Passport.Surname)
                .Where(x => x.Interviews.Any())
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<Candidate>> GetNotInterviewed()
        {
            return await context.Candidates
                .OrderBy(x => x.Passport.Surname)
                .Where(x => !(x.Interviews.Any()))
                .ToListAsync();
        }
    }
}
