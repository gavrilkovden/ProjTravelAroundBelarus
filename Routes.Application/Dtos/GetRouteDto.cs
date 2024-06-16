using Core.Application.Abstractions.Mappings;
using Travels.Domain;

namespace Routes.Application.Dtos
{
    public class GetRouteDto : IMapFrom<Route>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<GetAttractionInRouteDto> AttractionsInRoutes { get; set; } = new List<GetAttractionInRouteDto>();
    }
}
