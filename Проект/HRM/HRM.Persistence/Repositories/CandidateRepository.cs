using HRM.Application.BuisnessLogic;
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
            return await context.Interviews.Select(x=>x.Candidate)
                .OrderBy(x => x.Passport.Surname)
                .AsNoTracking().ToListAsync();
        }

        public async Task<List<Candidate>> GetNotInterviewed()
        {
            return await context.Candidates
                .OrderBy(x => x.Passport.Surname)
                .Where(x => !(x.Interviews.Any()))
                .ToListAsync();
        }

        public bool IsInterviewed(int candidateId)
        {
            var candidate = context.Candidates.Find(candidateId);
            if (GetInterviewed().Result.Where(x=>x.CandidateId == candidateId).Any())
                return true;
            else
                return false;
        }
    }
}
