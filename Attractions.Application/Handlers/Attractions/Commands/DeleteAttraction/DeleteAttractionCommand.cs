using Core.Auth.Application.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Commands.DeleteAttraction
{
    [RequestAuthorize]
    public class DeleteAttractionCommand : IRequest
    {
        public int Id { get; init; }
    }
}
