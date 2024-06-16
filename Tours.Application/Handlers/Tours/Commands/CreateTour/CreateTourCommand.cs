using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tours.Application.Dtos;

namespace Tours.Application.Handlers.Tours.Commands.CreateTour
{
    public class CreateTourCommand : IRequest<GetTourDto>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int RouteId { get; set; }
        public decimal? Price { get; set; }
    }
}
