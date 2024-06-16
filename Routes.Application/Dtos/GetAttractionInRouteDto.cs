using Core.Application.Abstractions.Mappings;
using System.Text.Json.Serialization;
using Travels.Domain;

namespace Routes.Application.Dtos
{
    public class GetAttractionInRouteDto : IMapFrom<AttractionInRoute>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int Order { get; set; }
        public decimal DistanceToNextAttraction { get; set; }
        public DateTime VisitDateTime { get; set; }
        public int RouteId { get; set; }
        public int AttractionId { get; set; }
    }
}
