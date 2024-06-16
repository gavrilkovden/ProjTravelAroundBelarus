using FluentValidation;
using Routes.Application.Handlers.Queries;

namespace Routes.Application.Handlers.Queries.GetRotesCount
{
    internal class GetRotesCountQueryValidator : AbstractValidator<GetRotesCountQuery>
    {
        public GetRotesCountQueryValidator()
        {
            RuleFor(e => e).IsValidListRouteFilter();
        }
    }
}
