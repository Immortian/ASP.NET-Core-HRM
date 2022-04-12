using HRM.Application.BusinessLogic;
using HRM.Domain;
using HRM.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Persistence.Repositories
{
    public class InterviewRepository : RepositoryBase<Interview>, IInterviewRepository
    {
        public InterviewRepository(HRMDBContext context) : base(context) 
        {
        }
    }
}
