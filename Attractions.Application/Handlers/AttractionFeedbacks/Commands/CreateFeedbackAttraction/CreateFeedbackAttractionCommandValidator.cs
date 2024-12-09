using Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction;
using FluentValidation;

namespace Travel.Application.Handlers.Attractions.Commands.CreateFeedbackAttraction
{
    public class CreateFeedbackAttractionCommandValidator : AbstractValidator<CreateFeedbackAttractionCommand>
    {
        public CreateFeedbackAttractionCommandValidator()
        {
            RuleFor(x => x.ValueRating).InclusiveBetween(1, 5).When(x => x.ValueRating.HasValue).WithMessage("ValueRating must be between 1 and 5.");
            RuleFor(e => e.AttractionId).GreaterThan(0).WithMessage("AttractionId must be greater than 0."); ;
            RuleFor(e => e.Comment).MaximumLength(1000).WithMessage("Comment must be 1000 characters or less."); ;
            RuleFor(x => x.Comment).NotEmpty().WithMessage("The comment should not be empty.").When(x => x.Comment != null);
        }
    }
}
