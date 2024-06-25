using FluentValidation;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.UpdateFeedbackAttraction
{
    public class UpdateFeedbackAttractionCommandValidator : AbstractValidator<UpdateFeedbackAttractionCommand>
    {
        public UpdateFeedbackAttractionCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
            RuleFor(x => x.ValueRating).InclusiveBetween(1, 5).When(x => x.ValueRating.HasValue).WithMessage("ValueRating must be between 1 and 5.");
            RuleFor(e => e.AttractionId).GreaterThan(0).WithMessage("AttractionId must be greater than 0."); ;
            RuleFor(e => e.Comment).MaximumLength(1000).WithMessage("Comment must be 1000 characters or less."); ;
            RuleFor(x => x.Comment).NotEmpty().WithMessage("The comment should not be empty.").When(x => x.Comment != null);
        }
    }
}
