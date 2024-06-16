using FluentValidation;
using Core.Application.ValidatorsExtensions;

namespace Attractions.Application.Handlers.Attractions.Queries.GetAttractions
{
    public class GetAttractionsQueryValidator : AbstractValidator<GetAttractionsQuery>
    {
        public GetAttractionsQueryValidator()
        {
            RuleFor(e => e).IsValidListAttractionFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
