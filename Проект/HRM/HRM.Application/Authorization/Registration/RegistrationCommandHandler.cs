using HRM.Application.Interfaces;
using HRM.Application.Exceptions;

namespace HRM.Application.Authorization.Registration
{
    public class RegistrationCommandHandler
    {
        private IUnitOfWork unitOfWork;
        public RegistrationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Registration(RegistrationCommand request)
        {
            if (!RegistrationCommandValidator.Validate(request))
                throw new ValidationException(nameof(RegistrationCommand));

            var current = new Domain.Authorization()
            {
                Username = request.Username,
                Password = request.Password,
            };

            if (!unitOfWork.Authorization.GetAllAsync().Result.Any())
            {
                current.Role = "Administrator";
                await unitOfWork.Authorization.CreateAsync(current);
                await unitOfWork.Save();
            }
            else
            {
                if (request.AuthCode == null)
                    throw new Exception(); //auth code is reqired
                var person = unitOfWork.Employee.GetByAuthCode(request.AuthCode);
                if (person == null)
                    throw new Exception(); //registration fail
                if(unitOfWork.Authorization.IsUsed(request.Username))
                    throw new Exception(); //choose another username exception
                
                current.EmployeeId = person.EmployeeId;

                if (unitOfWork.Department.IsEmployeeManager(person.EmployeeId))
                    current.Role = "Manager";
                else
                    current.Role = "User";
                await unitOfWork.Authorization.CreateAsync(current);
                await unitOfWork.Save();
            }
        }
    }
}
