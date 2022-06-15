using HRM.Application.BuisnessLogic.Base;
using File = HRM.Domain.File;

namespace HRM.Application.BuisnessLogic
{
    public interface IFileRepository : IRepositoryBase<File>
    {
        public IEnumerable<File> GetAllByEmployeeId(int employeeId);
        public IEnumerable<File> GetAllByPeriodId(int periodId);
        public File GetOne(int employeeId, int periodId);
        public IEnumerable<File> GetByDepartmentId(int departmentId, int periodId);
        public IEnumerable<File> GetFilesWithoutPrefab();
        public Task ClearStorage(string path);
    }
}
