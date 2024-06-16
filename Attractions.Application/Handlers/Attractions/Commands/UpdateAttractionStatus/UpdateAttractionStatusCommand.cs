using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Travel.Application.Dtos;

namespace Attractions.Application.Handlers.Attractions.Commands.UpdateAttractionStatus
{
    public class UpdateAttractionStatusCommand : IRequest<GetAttractionDto>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
