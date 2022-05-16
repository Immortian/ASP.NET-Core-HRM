using System;
using System.Collections.Generic;

namespace HRM.Desktop.Model
{
    public partial class Authorization
    {
        public int UserId { get; set; }
        public int? EmployeeId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
