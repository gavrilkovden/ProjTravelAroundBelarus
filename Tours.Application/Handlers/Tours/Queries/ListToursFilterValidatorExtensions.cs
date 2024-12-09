using FluentValidation;

namespace Tours.Application.Handlers.Tours.Queries
{
    public static class ListToursFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListToursFilter> IsValidListTourFilter<T>(this IRuleBuilder<T, ListToursFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListTourFilterValidator());
        }
    }
}
