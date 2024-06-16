using Core.Auth.Application.Attributes;
using MediatR;

namespace Routes.Application.Handlers.Commands.DeleteRoute
{
    [RequestAuthorize]
    public class DeleteRouteCommand : IRequest
    {
        public int Id { get; init; }
    }
}
