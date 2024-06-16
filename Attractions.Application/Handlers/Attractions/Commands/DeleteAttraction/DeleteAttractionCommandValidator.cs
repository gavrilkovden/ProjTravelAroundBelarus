using Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Handlers.Attractions.Commands.DeleteAttraction
{
    public class DeleteAttractionCommandValidator : AbstractValidator<DeleteAttractionCommand>
    {
        public DeleteAttractionCommandValidator()
        {
            RuleFor(e => e.Id).GreaterThan(0);
        }
    }
}
