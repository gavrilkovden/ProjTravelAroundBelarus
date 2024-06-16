using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Commands.CreateFeedbackTour
{
    internal class CreateFeedbackTourCommandValidator : AbstractValidator<CreateFeedbackTourCommand>
    {
        public CreateFeedbackTourCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
            RuleFor(x => x.Value).InclusiveBetween(1, 5).When(x => x.Value.HasValue).WithMessage("Value must be between 1 and 5.");
            RuleFor(e => e.TourId).GreaterThan(0);
            RuleFor(e => e.Comment).MaximumLength(1000).NotEmpty().WithMessage("The comment should not be empty.").When(x => x.Comment != null);
        }
    }
}

