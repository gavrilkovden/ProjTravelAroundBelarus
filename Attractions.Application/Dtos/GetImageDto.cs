using Core.Application.Abstractions.Mappings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos;
using Travels.Domain;

namespace Attractions.Application.Dtos
{
    public class GetImageDto : IMapFrom<Image>
    {
        public int Id { get; set; }
        public int AttractionId { get; set; }
        public string ImagePath { get; set; }
        public bool IsApproved { get; set; }
        public Guid UserId { get; set; } 
    }
}
