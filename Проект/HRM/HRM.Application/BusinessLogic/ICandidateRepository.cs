using HRM.Application.BuisnessLogic.Base;
using HRM.Domain;

namespace HRM.Application.BuisnessLogic
{
    public interface ICandidateRepository : IRepositoryBase<Candidate>
    {
        Task<List<Candidate>> GetNotInterviewed();
        Task<List<Candidate>> GetInterviewed();
        bool IsInterviewed(int candidateId);
    }
}
