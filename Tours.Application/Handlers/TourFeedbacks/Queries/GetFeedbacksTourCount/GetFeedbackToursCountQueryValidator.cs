using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTourCount
{
    internal class GetFeedbackToursCountQueryValidator : AbstractValidator<GetFeedbackToursCountQuery>
    {
        public GetFeedbackToursCountQueryValidator()
        {
            RuleFor(e => e).IsValidListFeedbackAttractionFilter();
        }
    }
}
