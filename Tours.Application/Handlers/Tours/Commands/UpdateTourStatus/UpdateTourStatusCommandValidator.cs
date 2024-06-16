using FluentValidation;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTourStatus
{

    public class UpdateTourStatusCommandValidator : AbstractValidator<UpdateTourStatusCommand>
    {
        public UpdateTourStatusCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThanOrEqualTo(0);
        }
    }
}
