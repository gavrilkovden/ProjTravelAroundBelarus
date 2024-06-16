using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.AttractionFeedbacks.Commands.CreateFeedbackAttraction
{
    public class CreateFeedbackAttractionCommand : IRequest<GetFeedbackAttractionDto>
    {
        public int? ValueRating { get; set; } = null;
        public int AttractionId { get; set; }
        public string? Comment { get; set; } = null;
    }
}
