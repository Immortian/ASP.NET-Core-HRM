using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace HRM.Application.Dismissing
{
    public class DismissalCommandValidators : AbstractValidator<DismissingCommand>
    {
        public DismissalCommandValidators()
        {
            RuleFor(c => c.Payments).GreaterThanOrEqualTo(0);
        }
    }
}
