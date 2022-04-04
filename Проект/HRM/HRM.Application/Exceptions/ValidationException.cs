using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string key) : base($"{key} validation failed.") { }
    }
}
