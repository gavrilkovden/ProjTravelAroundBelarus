using FluentValidation;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateImageApproveStatus
{
    public class UpdateImageApproveStatusCommandValidator : AbstractValidator<UpdateImageApproveStatusCommand>
    {
        public UpdateImageApproveStatusCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
