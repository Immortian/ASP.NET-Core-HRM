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
    public class DismissalRepository : RepositoryBase<Dismissal>, IDismissalRepository
    {
        public DismissalRepository(HRMDBContext context) : base(context)
        {

        }
    }
}
