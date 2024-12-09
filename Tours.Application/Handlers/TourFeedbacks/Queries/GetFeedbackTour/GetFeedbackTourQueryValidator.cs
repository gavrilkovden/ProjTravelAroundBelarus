using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Queries.GetFeedbackTour
{
    internal class GetFeedbackTourQueryValidator : AbstractValidator<GetFeedbackTourQuery>
    {
        public GetFeedbackTourQueryValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
