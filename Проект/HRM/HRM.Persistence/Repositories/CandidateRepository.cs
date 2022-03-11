using HRM.Application.BusinessLogic.Candidates;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        public CandidateRepository(HRMDBContext context) : base(context)
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
