using FluentValidation;

namespace Routes.Application.Handlers.Commands.UpdateRoute
{
    public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
    {
        public UpdateRouteCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
