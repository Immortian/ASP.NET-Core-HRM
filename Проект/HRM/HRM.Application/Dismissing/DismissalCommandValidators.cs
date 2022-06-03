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
