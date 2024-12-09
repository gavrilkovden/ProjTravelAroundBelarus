using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using FluentValidation;
using Core.Application.ValidatorsExtensions;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractions
{
    public class GetFeedbackAttractionsQueryValidator : AbstractValidator<GetFeedbackAttractionsQuery>
    {
        public GetFeedbackAttractionsQueryValidator()
        {
            RuleFor(e => e).IsValidListFeedbackAttractionFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
