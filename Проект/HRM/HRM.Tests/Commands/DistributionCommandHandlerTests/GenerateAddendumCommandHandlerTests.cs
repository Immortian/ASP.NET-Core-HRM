using HRM.Application.WorkLoadDistribution.GenerateAddendum;
using HRM.Tests.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.DistributionCommandHandlerTests
{
    public class GenerateAddendumCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task GenerateAddendumCommandHandlerTests_Success()
        {
            //Arrange
            var handler = new GenerateAddendumCommandHandler(unitOfWork);

            //Act
            foreach (var employeeLoad in unitOfWork.EmployeeWorkLoad.GetByPeriodId(1))
            {
                await handler.GenerateAddendum(
                new GenerateAddendumCommand
                {
                    PeriodId = 1,
                    EmployeeId = employeeLoad.EmployeeId,
                    WorkLoad = employeeLoad.WorkLoadHours
                });
            }

            //Assert
            var res = unitOfWork.File.GetAllByPeriodId(1);
            Assert.Equal(2, res.Count());
            Assert.NotNull(res.FirstOrDefault().Data);
        }
    }
}
