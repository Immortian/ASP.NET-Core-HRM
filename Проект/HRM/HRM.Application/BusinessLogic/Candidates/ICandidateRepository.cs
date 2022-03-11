using HRM.Domain;

namespace HRM.Application.BusinessLogic.Candidates
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetNotInterviewed();
        Task<List<Candidate>> GetInterviewed();
    }
}
