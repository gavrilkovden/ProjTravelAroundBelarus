using MediatR;
using Routes.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Handlers.Commands.CreateAttractionInRoute
{
    public class CreateAttractionInRouteCommand : IRequest<GetAttractionInRouteDto>
    {
        public int Order { get; set; }
        public decimal DistanceToNextAttraction { get; set; }
        public DateTime VisitDateTime { get; set; }
        public int RouteId { get; set; }
        public int AttractionId { get; set; }
    }
}
