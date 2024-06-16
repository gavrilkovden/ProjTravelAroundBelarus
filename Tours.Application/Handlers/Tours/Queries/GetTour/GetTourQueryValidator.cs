using FluentValidation;

namespace Tours.Application.Handlers.Tours.Queries.GetTour
{
    internal class GetTourQueryValidator : AbstractValidator<GetTourQuery>
    {
        public GetTourQueryValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
