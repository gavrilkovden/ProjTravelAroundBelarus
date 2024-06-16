using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.DeleteFeedbackTour
{
    public class DeleteFeedbackTourCommandValidator : AbstractValidator<DeleteFeedbackTourCommand>
    {
        public DeleteFeedbackTourCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
