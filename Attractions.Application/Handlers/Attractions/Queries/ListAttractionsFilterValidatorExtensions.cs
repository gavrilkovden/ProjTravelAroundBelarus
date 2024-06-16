using FluentValidation;

namespace Attractions.Application.Handlers.Attractions.Queries
{
    public static class ListAttractionsFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListAttractionsFilter> IsValidListAttractionFilter<T>(this IRuleBuilder<T, ListAttractionsFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListAttractionFilterValidator());
        }
    }
}
