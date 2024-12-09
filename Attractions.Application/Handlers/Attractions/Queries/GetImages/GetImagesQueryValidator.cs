using Attractions.Application.Handlers.Attractions.Queries.ImageFilter;
using Core.Application.ValidatorsExtensions;
using FluentValidation;

namespace Attractions.Application.Handlers.Attractions.Queries.GetImages
{
    public class GetImagesQueryValidator : AbstractValidator<GetImagesQuery>
    {
        public GetImagesQueryValidator()
        {
            RuleFor(e => e).IsValidListImageFilter();
            RuleFor(e => e).IsValidPaginationFilter();
        }
    }
}
