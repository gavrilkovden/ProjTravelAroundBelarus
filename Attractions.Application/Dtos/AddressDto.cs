﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travels.Domain;

namespace Travel.Application.Dtos
{
    public class AddressDto
    {
        public string? Street { get; set; }
        public CityEnum City { get; set; }
        public RegionEnum Region { get; set; }

        public static implicit operator AddressDto(Address v)
        {
            throw new NotImplementedException();
        }
    }
}
