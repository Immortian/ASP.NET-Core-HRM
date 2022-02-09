using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Application.BusinessLogic.Authorizations.Commands.UpdateAuthorization
{
    public class UpdateAuthorizationCommand
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
