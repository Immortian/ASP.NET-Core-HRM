using HRM.Application.WorkLoadDistribution;
using HRM.Application.WorkLoadDistribution.CreateDistribution;
using HRM.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.DistributionCommandHandlerTests
{
    public class CreateDistributionCommandHandlerTests :TestCommandBase
    {
        [Fact]
        public async Task CreateDistributionCommandHandlerTests_Success1()
        {
            //Arrange
            var handler = new CreateDistributionCommandHandler(unitOfWork);
            var monthlyHours = 260;

            //Act
            await handler.Distribute(
                new CreateDistributionCommand
                {
                    MonthlyHours = monthlyHours,
                    Options = null
                });

            //Assert
            Assert.NotNull(await Context.Periods.SingleOrDefaultAsync(period =>
            period.TotalWorkLoadHours == monthlyHours));
            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
            load.EmployeeId == 1 &&
            load.WorkLoadHours == 130));
        }

        [Fact]
        public async Task CreateDistributionCommandHandlerTests_Success2()
        {
            //Arrange
            var handler = new CreateDistributionCommandHandler(unitOfWork);
            var monthlyHours = 0;
            var options = new List<DistributionOption>();
            options.Add(new DistributionOption
            {
                DepartmentId = 1,
                StaticHours = 130                
            });
            options.Add(new DistributionOption
            {
                DepartmentId = 2,
                StaticHours = 140
            });

            //Act
            await handler.Distribute(
                new CreateDistributionCommand
                {
                    MonthlyHours = monthlyHours,
                    Options = options
                });

            //Assert
            Assert.NotNull(await Context.Periods.SingleOrDefaultAsync(period =>
            period.TotalWorkLoadHours == 270));

            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
           load.EmployeeId == 1 &&
           load.WorkLoadHours == 130));

            Assert.NotNull(await Context.EmployeeWorkLoads.SingleOrDefaultAsync(load =>
           load.EmployeeId == 2 &&
           load.WorkLoadHours == 140));
        }
    }
}
