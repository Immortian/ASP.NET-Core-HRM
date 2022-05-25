using HRM.Application.Interviewing;
using HRM.Tests.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HRM.Tests.Commands.HRManagement
{
    public class InterviewingCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task InterviewingCommandHandlerTests_Success()
        {
            //Arrange
            var handler = new InterviewingCommandHandler(unitOfWork);
            var id = 3;
            var isPassed = true;
            var passDate = DateTime.Now;
            var departmentId = 1;

            //Act
            await handler.TakeInterview(
            new InterviewingCommand
            {
                CandidateId = id,
                IsPassed = isPassed,
                PassDate = passDate,
                DepartmentId = departmentId
            });

            //Assert
            var res = await Context.Interviews.SingleOrDefaultAsync(interview =>
                interview.CandidateId == id &&
                interview.IsPassed == isPassed &&
                interview.Date == passDate);
            Assert.NotNull(res);
            Assert.NotNull(
                await Context.Employees.SingleOrDefaultAsync(employee =>
                employee.InterviewId == res.InterviewId));
        }
    }
}
