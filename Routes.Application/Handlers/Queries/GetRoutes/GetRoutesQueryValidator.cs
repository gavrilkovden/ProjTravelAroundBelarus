using FluentValidation;
using Core.Application.ValidatorsExtensions;

namespace Routes.Application.Handlers.Queries.GetRoutes
{
    public class GetRoutesQueryValidator : AbstractValidator<GetRoutesQuery>
    {
        public GetRoutesQueryValidator()
        {
            RuleFor(e => e).IsValidListRouteFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
