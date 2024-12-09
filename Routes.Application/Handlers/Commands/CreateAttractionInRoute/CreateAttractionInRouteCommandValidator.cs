using FluentValidation;

namespace Routes.Application.Handlers.Commands.CreateAttractionInRoute
{
    public class CreateAttractionInRouteCommandValidator : AbstractValidator<CreateAttractionInRouteCommand>
    {
        public CreateAttractionInRouteCommandValidator()
        {
            RuleFor(x => x.Order).GreaterThan(0).WithMessage("Order must be greater than 0");

            RuleFor(x => x.DistanceToNextAttraction).GreaterThanOrEqualTo(0).WithMessage("Distance to next attraction must be greater than or equal 0");

            RuleFor(x => x.VisitDateTime).GreaterThan(DateTime.UtcNow).WithMessage("Visit date and time must be in the future");

            RuleFor(x => x.RouteId).GreaterThan(0).WithMessage("RouteId must be greater than 0");

            RuleFor(x => x.AttractionId).GreaterThan(0).WithMessage("AttractionId must be greater than 0");
        }
    }
}
