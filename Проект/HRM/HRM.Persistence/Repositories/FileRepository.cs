using HRM.Application.BuisnessLogic;
using HRM.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace HRM.Persistence.Repositories
{
    public class FileRepository : RepositoryBase<Domain.File>, IFileRepository
    {
        public FileRepository(HRMDBContext context) : base(context)
        {

        }

        public Task ClearStorage(string path)
        {
            foreach(var file in System.IO.Directory.GetFiles(path))
            {
                System.IO.File.Delete(file);
            }
            return Task.CompletedTask;
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
