using Core.Auth.Application.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Commands.DeleteImage
{
    [RequestAuthorize]
    public class DeleteImageCommand : IRequest
    {
        public int Id { get; init; }
    }
}
