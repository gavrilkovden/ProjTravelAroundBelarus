using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter
{
    public static class ListFeedbackAttractionsFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListFeedbackAttractionsFilter> IsValidListFeedbackAttractionFilter<T>(this IRuleBuilder<T, ListFeedbackAttractionsFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListFeedbackAttractionFilterValidator());
        }
    }
}
