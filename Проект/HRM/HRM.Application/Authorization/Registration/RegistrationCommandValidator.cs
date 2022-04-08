using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Authorization.Registration
{
    public static class RegistrationCommandValidator
    {
        public static bool Validate(RegistrationCommand request)
        {
            if (request.Username.Contains(' '))
                return false;
            if (request.Username.Length < 5 || request.Username.Length > 20)
                return false;
            if (request.Password.Contains(' '))
                return false;
            if (request.Password.Length < 5)
                return false;
            return true;
        }
    }
}
