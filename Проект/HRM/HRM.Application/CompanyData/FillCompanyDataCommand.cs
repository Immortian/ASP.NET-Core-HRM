using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.CompanyData
{
    public class FillCompanyDataCommand
    {
        public string? CompanyName { get; set; }
        public string? DirectorName { get; set; }
        public string? CompanyAddress { get; set; }
        public string? INN { get; set; }
        public string? BIK { get; set; }
        public string? KPP { get; set; }
        public string? PAcc { get; set; }
        public string? CAcc { get; set; }
        public string? Bank { get; set; }
    }
}
