using FluentValidation;

namespace Tours.Application.Handlers.Tours.Queries
{
    internal class BaseListTourFilterValidator : AbstractValidator<ListToursFilter>
    {
        public BaseListTourFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}
