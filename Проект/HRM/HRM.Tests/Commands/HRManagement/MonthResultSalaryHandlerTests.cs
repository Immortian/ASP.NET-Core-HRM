using HRM.Application.Salary;
using HRM.Tests.Common;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.HRManagement
{
    public class MonthResultSalaryHandlerTests : TestCommandBase
    {
        /// <summary>
        /// employee 1 отработал 100 часов из 120 в отделе со ставкой 300р/час
        /// employee 2 отработал 140 часов из 120 в отделе со ставкой 300р/час
        /// </summary>
        /// <returns>employee 2 получит зарплату только за 120 часов</returns>
        [Fact]
        public async Task MonthResultSalaryHandlerTests_Success()
        {
            //Arrange
            var handler = new MonthResultSalaryHandler(unitOfWork);

            //Act
            await handler.Execute(null);

            //Assert
            Assert.Equal(30000, unitOfWork.EmployeeWorkLoad.GetByIdAsync(1).Result.ResultSalary);
            Assert.Equal(36000, unitOfWork.EmployeeWorkLoad.GetByIdAsync(2).Result.ResultSalary);
        }
    }
}
