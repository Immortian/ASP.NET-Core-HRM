using HRM.Application.BuisnessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;

namespace HRM.Persistence.Repositories
{
    public class InterviewRepository : RepositoryBase<Interview>, IInterviewRepository
    {
        public InterviewRepository(HRMDBContext context) : base(context) 
        {
        }
    }
}
