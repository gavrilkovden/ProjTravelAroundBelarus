using FluentValidation;

namespace Tours.Application.Handlers.Tours.Commands.UpdateTour
{
    public class UpdateTourCommandValidator : AbstractValidator<UpdateTourCommand>
    {
        public UpdateTourCommandValidator()
        {
            RuleFor(x => x.Id)
             .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(255).WithMessage("Name must not exceed 255 characters");

            RuleFor(x => x.RouteId)
                .GreaterThan(0).WithMessage("RouteId must be greater than 0");

            RuleFor(x => x.Price)
            .GreaterThan(0).When(x => x.Price.HasValue).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }
    }
}
