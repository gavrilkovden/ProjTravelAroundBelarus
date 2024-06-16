using FluentValidation;

namespace Tours.Application.Handlers.TourFeedbacks.Queries
{
    internal class BaseListFeedbackTourFilterValidator : AbstractValidator<ListFeedbackToursFilter>
    {
        public BaseListFeedbackTourFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}
