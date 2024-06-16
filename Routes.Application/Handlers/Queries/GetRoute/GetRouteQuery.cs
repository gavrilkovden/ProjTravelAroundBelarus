using Core.Auth.Application.Attributes;
using MediatR;
using Routes.Application.Dtos;

namespace Routes.Application.Handlers.Queries.GetRoute
{
    [RequestAuthorize]
    public class GetRouteQuery : IRequest<GetRouteDto>
    {
        public int Id { get; init; }
    }
}
