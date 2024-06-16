using FluentValidation;

namespace Routes.Application.Handlers.Queries.GetRoutes
{
    internal class GetRoutesQueryValidator : AbstractValidator<GetRoutesQuery>
    {
        public GetRoutesQueryValidator()
        {
            RuleFor(e => e).IsValidListRouteFilter();
        }
    }
}
