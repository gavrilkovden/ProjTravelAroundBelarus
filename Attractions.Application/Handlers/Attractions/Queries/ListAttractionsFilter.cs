﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Handlers.Attractions.Queries
{
    public class ListAttractionsFilter
    {
        public string? FreeText { get; init; }
        public bool? IsApproved { get; init; }
    }
}