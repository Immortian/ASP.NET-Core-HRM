using CRM.Domain;

namespace CRM.Application.BusinessLogic.Candidates
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetNotInterviewed();
        Task<List<Candidate>> GetInterviewed();
    }
}
