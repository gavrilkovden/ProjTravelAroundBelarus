using FluentValidation;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Queries.GetTours
{
    internal class GetToursQueryValidator : AbstractValidator<GetToursQuery>
    {
        public GetToursQueryValidator()
        {
            RuleFor(e => e).IsValidListTourFilter();
        }
    }
}
