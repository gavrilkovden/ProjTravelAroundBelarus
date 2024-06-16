using FluentValidation;

namespace Routes.Application.Handlers.Queries.GetRoute
{
    internal class GetRouteQueryValidator : AbstractValidator<GetRouteQuery>
    {
        public GetRouteQueryValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
