using Attractions.Application.Handlers.AttractionFeedbacks.Queries.GetFeedbackAttraction;
using FluentValidation;

namespace Travel.Application.Handlers.Attractions.Queries.GetFeedbackAttraction
{
    public class GetFeedbackAttractionQueryValidator : AbstractValidator<GetFeedbackAttractionQuery>
    {
        public GetFeedbackAttractionQueryValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
