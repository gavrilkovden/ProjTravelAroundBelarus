using FluentValidation;

namespace Routes.Application.Handlers.Queries
{
    internal class BaseListRoteFilterValidator : AbstractValidator<ListRoutesFilter>
    {
        public BaseListRoteFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}