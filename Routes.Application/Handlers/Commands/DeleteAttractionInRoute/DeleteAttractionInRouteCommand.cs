using Core.Auth.Application.Attributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Handlers.Commands.DeleteAttractionInRoute
{
    [RequestAuthorize]
    public class DeleteAttractionInRouteCommand : IRequest
    {
        public int Id { get; init; }
    }
}
