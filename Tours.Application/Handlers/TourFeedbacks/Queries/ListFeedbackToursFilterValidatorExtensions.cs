using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Queries
{
    internal static class ListFeedbackToursFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListFeedbackToursFilter> IsValidListFeedbackAttractionFilter<T>(this IRuleBuilder<T, ListFeedbackToursFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListFeedbackTourFilterValidator());
        }
    }
}
