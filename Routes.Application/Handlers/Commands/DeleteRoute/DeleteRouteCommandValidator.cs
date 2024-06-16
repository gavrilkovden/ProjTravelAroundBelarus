using FluentValidation;

namespace Routes.Application.Handlers.Commands.DeleteRoute
{
    public class DeleteRouteCommandValidator : AbstractValidator<DeleteRouteCommand>
    {
        public DeleteRouteCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
