using FluentValidation;

namespace Tours.Application.Handlers.Tours.Queries.GetToursCount
{
    internal class GetToursCountQueryValidator : AbstractValidator<GetToursCountQuery>
    {
        public GetToursCountQueryValidator()
        {
            RuleFor(e => e).IsValidListTourFilter();
        }
    }
}
