using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbacksTour
{
    internal class GetFeedbackToursQueryValidator : AbstractValidator<GetFeedbackToursQuery>
    {
        public GetFeedbackToursQueryValidator()
        {
            RuleFor(e => e).IsValidListFeedbackAttractionFilter();
        }
    }
}
