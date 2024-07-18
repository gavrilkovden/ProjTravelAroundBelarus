using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Commands.DeleteImage
{
    public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
