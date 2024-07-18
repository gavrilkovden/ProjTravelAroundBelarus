using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries.ImageFilter
{
    public static class ListImagesFilterValidatorExtensions
    {
        internal static IRuleBuilderOptions<T, ListImagesFilter> IsValidListImageFilter<T>(this IRuleBuilder<T, ListImagesFilter> ruleBuilder)
        {
            return ruleBuilder
                .SetValidator(new BaseListImageFilterValidator());
        }
    }
}
