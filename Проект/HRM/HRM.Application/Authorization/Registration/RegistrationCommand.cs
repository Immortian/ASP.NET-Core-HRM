using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Authorization.Registration
{
    public class RegistrationCommand
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string AuthCode { get; set; }
    }
}
