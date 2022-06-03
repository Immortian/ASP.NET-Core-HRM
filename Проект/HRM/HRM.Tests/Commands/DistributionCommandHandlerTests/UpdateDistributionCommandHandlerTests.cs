using HRM.Application.WorkLoadDistribution;
using HRM.Application.WorkLoadDistribution.UpdateDistribution;
using HRM.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.DistributionCommandHandlerTests
{
    public class UpdateDistributionCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateDistributionCommandHandlerTests_Success1()
        {
            //Arrange
            var handler = new UpdateDistributionCommandHandler(unitOfWork);
            var monthlyHours = 300;

            //Act
            await handler.UpdateDistribution(
                new UpdateDistributionCommand
                {
                    PeriodId = 1,
                    MonthlyHours = monthlyHours,
                    Options = null
                });

            //Assert
            Assert.NotNull(await Context.Periods.SingleOrDefaultAsync(period =>
            period.TotalWorkLoadHours == monthlyHours));
            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
            load.EmployeeId == 1 &&
            load.WorkLoadHours == 150));
        }

        [Fact]
        public async Task UpdateDistributionCommandHandlerTests_Success2()
        {
            //Arrange
            var handler = new UpdateDistributionCommandHandler(unitOfWork);
            var monthlyHours = 0;

            //Act
            await handler.UpdateDistribution(
                new UpdateDistributionCommand
                {
                    PeriodId = 1,
                    MonthlyHours = monthlyHours,
                    Options = new List<DistributionOption>
                    {
                        new DistributionOption
                        {
                            DepartmentId = 1,
                            StaticHours = 140
                        },
                        new DistributionOption
                        {
                            DepartmentId = 2,
                            StaticHours = 160
                        }
                    }
                });

            //Assert
            Assert.NotNull(await Context.Periods.SingleOrDefaultAsync(period =>
            period.TotalWorkLoadHours == 300));
            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
            load.EmployeeId == 1 &&
            load.WorkLoadHours == 140));
            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
            load.EmployeeId == 2 &&
            load.WorkLoadHours == 160));
        }
    }
}
