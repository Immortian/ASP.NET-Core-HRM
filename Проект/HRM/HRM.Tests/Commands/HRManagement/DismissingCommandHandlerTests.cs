using HRM.Application.Dismissing;
using HRM.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.HRManagement
{
    public class DismissingCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DismissingCommandHandlerTests_Success()
        {
            //Arrange
            var handler = new DismissingCommandHandler(unitOfWork);
            var employeeId = 1;
            var reason = "else";
            var Payments = 0;

            //Act
            await handler.Dismiss(
                new DismissingCommand
                {
                    EmployeeId = employeeId,
                    Reason = reason,
                    Payments = Payments
                });

            //Assert
            Assert.NotNull(unitOfWork.Dismissal.FirstAsync());
            Assert.Null(await unitOfWork.Employee.GetByIdAsync(1));
        }
    }
}
