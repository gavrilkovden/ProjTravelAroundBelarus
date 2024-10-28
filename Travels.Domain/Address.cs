using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Travels.Domain
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public RegionEnum Region { get; set; }

        public ICollection<Attraction> Attractions { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegionEnum
    {
        Гомельская, // Гомельская область
        Витебская, // Витебская область
        Гродненская, // Гродненская область
        Минская, // Минская область
        Могилевская, // Могилевская область
        Брестская // Брестская область
    }
}
