using HRM.Application.BusinessLogic;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Persistence.Repositories
{
    public class FileRepository : RepositoryBase<Domain.File>, IFileRepository
    {
        public FileRepository(HRMDBContext context) : base(context)
        {

        }

        public IEnumerable<Domain.File> GetAllByEmployeeId(int employeeId)
        {
            return context.Files.Where(x => x.EmployeeId == employeeId).AsNoTracking().ToArray();
        }

        public IEnumerable<Domain.File> GetAllByPeriodId(int periodId)
        {
            return context.Files.Where(x=>x.PeriodId == periodId).AsNoTracking().ToArray();
        }

        public IEnumerable<Domain.File> GetByDepartmentId(int departmentId, int periodId)
        {
            return context.Files.Where(x=>x.DepartmentId == departmentId && x.PeriodId == periodId).AsNoTracking().ToArray();
        }

        public Domain.File GetOne(int employeeId, int periodId)
        {
            return context.Files.Where(x=>x.EmployeeId == employeeId && x.PeriodId == periodId).AsNoTracking().FirstOrDefault();
        }
    }
}
