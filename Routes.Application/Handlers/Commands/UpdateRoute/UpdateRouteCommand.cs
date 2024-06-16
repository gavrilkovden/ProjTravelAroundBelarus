using MediatR;
using Routes.Application.Dtos;
using System.Text.Json.Serialization;

namespace Routes.Application.Handlers.Commands.UpdateRoute
{
    public class UpdateRouteCommand : IRequest<GetRouteDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<GetAttractionInRouteDto> AttractionsInRoutes { get; set; } = new List<GetAttractionInRouteDto>();
    }
}
