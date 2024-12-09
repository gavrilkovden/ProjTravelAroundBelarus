using FluentValidation;

namespace Tours.Application.Handlers.Tours.Commands.CreateTour
{
    public class CreateTourCommandValidator : AbstractValidator<CreateTourCommand>
    {
        public CreateTourCommandValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Name is required")
           .MaximumLength(255).WithMessage("Name must not exceed 255 characters");

            RuleFor(x => x.RouteId)
                .GreaterThan(0).WithMessage("RouteId must be greater than 0");

            RuleFor(x => x.Price)
            .GreaterThan(0).When(x => x.Price.HasValue).WithMessage("Price must be greater than or equal to 0.");
        }
    }
}
