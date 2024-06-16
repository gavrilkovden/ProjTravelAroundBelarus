using FluentValidation;

namespace Tours.Application.Handlers.Tours.Commands.DeleteTour
{
    public class DeleteTourCommandValidator : AbstractValidator<DeleteTourCommand>
    {
        public DeleteTourCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
