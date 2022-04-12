using HRM.Application.Interfaces;
using HRM.Domain;

namespace HRM.Application.Interviewing
{
    public class InterviewingCommandHandler
    {
        private readonly IUnitOfWork unitOfWork;
        public InterviewingCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task TakeInterview(InterviewingCommand request)
        {
            var candidate = await unitOfWork.Candidate.GetByIdAsync(request.CandidateId);
            if (unitOfWork.Candidate.IsInterviewed(candidate.CandidateId))
                throw new Exception(); //Already interviewd
            var current = new Interview()
            {
                CandidateId = candidate.CandidateId,
                Date = request.PassDate,
                IsPassed = request.IsPassed
            };
            await unitOfWork.Interview.CreateAsync(current);
            if(current.IsPassed)
            {
                await unitOfWork.Employee.CreateAsync(new Employee
                {
                    PassportId = candidate.PassportId,
                    DepartmentId = request.DepartmentId,
                    ContactDataId = candidate.ContactDataId,
                    InterviewId = current.InterviewId,
                    Active = true,
                    AuthorizationCode = Guid.NewGuid().ToString()
                });
            }
            await unitOfWork.Save();
        }
    }
}
