using System.ComponentModel.DataAnnotations;

namespace HRM.Desktop.Commands
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
