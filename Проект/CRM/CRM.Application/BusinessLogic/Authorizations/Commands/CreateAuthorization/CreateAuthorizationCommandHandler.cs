using CRM.Application.Interfaces;
using CRM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Authorizations.Commands.CreateAuthorization
{
    public class CreateAuthorizationCommandHandler
    {
        private readonly ICRMDBContext _dbContext;
        public CreateAuthorizationCommandHandler(ICRMDBContext dBContext) =>
            _dbContext = dBContext;

        public async Task Handle(CreateAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var employee = _dbContext.Employees.Where(x => x.Authorization_code == request.SecretCode).FirstOrDefault();
            if (employee == null) 
            {
                //exception таких сотрудников нет
                //а как внедрять на предприятие?
                return;
            }
            if(employee.Authorizations.FirstOrDefault() != null)
            {
                //exception сотрудник уже зарегистрировался
                return;
            }
            var authorization = new Authorization
            {
                Username = request.Username,
                Password = request.Password,
                EmployeeId = employee.EmployeeId,
                Employee = employee,
                Role = employee.Department.Manager_id == employee.EmployeeId? "Manager" : "Employee"
            };

            await _dbContext.Authorizations.AddAsync(authorization);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}
