﻿using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Caches
{
    public class RoutesCountMemoryCache : BaseCache<int>;
}