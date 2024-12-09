using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries.ImageFilter
{
    internal class BaseListImageFilterValidator : AbstractValidator<ListImagesFilter>
    {
        public BaseListImageFilterValidator()
        {
            RuleFor(e => e.FreeText).MaximumLength(50).When(e => e.FreeText is not null);
        }
    }
}
