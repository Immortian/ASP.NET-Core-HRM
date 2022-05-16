using System.ComponentModel.DataAnnotations;

namespace HRM.Desktop.Commands
{
    public class LoginCommand
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
