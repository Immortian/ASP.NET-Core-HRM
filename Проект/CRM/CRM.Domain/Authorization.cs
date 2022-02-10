using System;
using System.Collections.Generic;

namespace CRM.Domain
{
    public partial class Authorization
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual Employee? Employee { get; set; }
    }
}
