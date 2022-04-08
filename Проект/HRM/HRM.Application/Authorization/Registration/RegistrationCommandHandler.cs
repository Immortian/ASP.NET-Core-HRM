using HRM.Application.Interfaces;
using HRM.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var person = unitOfWork.Employee.GetByAuthCode(request.AuthCode);
                if (person == null)
                    throw new Exception(); //registration fail
                else if(unitOfWork.Authorization.IsUsed(request.Username))
                    throw new Exception(); //choose another username exception
                else
                {
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
}
