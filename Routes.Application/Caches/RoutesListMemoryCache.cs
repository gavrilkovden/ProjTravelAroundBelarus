﻿using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Routes.Application.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Caches
{
    public class RoutesListMemoryCache : BaseCache<BaseListDto<GetRoutesDto>>;
}