using FluentValidation;

namespace Attractions.Application.Handlers.Attractions.Queries
{
    internal class BaseListAttractionFilterValidator : AbstractValidator<ListAttractionsFilter>
    {
        public BaseListAttractionFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}
