using Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus;
using FluentValidation;

namespace Travel.Application.Handlers.Attractions.Commands.UpdateAttractionStatus
{
    public class UpdateAttractionStatusCommandValidator : AbstractValidator<UpdateAttractionStatusCommand>
    {
        public UpdateAttractionStatusCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
