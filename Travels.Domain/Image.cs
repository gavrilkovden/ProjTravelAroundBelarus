using Core.Users.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travels.Domain
{
    public class Image
    {
        public int Id { get; set; }
        public int AttractionId { get; set; }
        public string ImagePath { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCover { get; set; }
        public Guid UserId { get; set; }

        public Attraction Attraction { get; set; }
        public ApplicationUser User { get; set; }
    }
}
