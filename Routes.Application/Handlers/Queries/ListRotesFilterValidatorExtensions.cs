using FluentValidation;

namespace Routes.Application.Handlers.Queries
{
    public static class ListRotesFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListRoutesFilter> IsValidListRouteFilter<T>(this IRuleBuilder<T, ListRoutesFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListRoteFilterValidator());
        }
    }
}
