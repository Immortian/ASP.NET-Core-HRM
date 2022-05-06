using HRM.Application.BuisnessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = HRM.Domain.File;

namespace HRM.Application.BusinessLogic
{
    public interface IFileRepository : IRepositoryBase<File>
    {
        public IEnumerable<File> GetAllByEmployeeId(int employeeId);
        public IEnumerable<File> GetAllByPeriodId(int periodId);
        public File GetOne(int employeeId, int periodId);
        public IEnumerable<File> GetByDepartmentId(int departmentId, int periodId);
    }
}
