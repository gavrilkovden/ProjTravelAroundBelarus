using Attractions.Application.Handlers.AttractionFeedbacks.Queries.FeedbackAttractionFilter;
using FluentValidation;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttractionsCount
{
    public class GetFeedbackAttractionsCountQueryValidator : AbstractValidator<GetFeedbackAttractionsCountQuery>
    {
        public GetFeedbackAttractionsCountQueryValidator()
        {
            RuleFor(e => e).IsValidListFeedbackAttractionFilter();
        }
    }
}
