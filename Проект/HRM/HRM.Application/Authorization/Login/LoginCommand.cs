using System.ComponentModel.DataAnnotations;

namespace HRM.Application.Authorization.Login
{
    public class LoginCommand
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
