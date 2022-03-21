using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Exeptions
{
    public class ValidationExeption : Exception
    {
        public ValidationExeption(string key) : base($"{key} validation failed.") { }
    }
}
