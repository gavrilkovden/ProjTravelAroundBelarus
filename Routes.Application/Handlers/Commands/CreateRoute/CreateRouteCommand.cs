using MediatR;
using Routes.Application.Dtos;

namespace Routes.Application.Handlers.Commands.CreateRoute
{
    public class CreateRouteCommand : IRequest<GetRouteDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
