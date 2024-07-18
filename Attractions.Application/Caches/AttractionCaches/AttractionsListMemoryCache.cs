using Attractions.Application.Dtos;
using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Travel.Application.Dtos;

namespace Attractions.Application.Caches.AttractionCaches;

public class AttractionsListMemoryCache : BaseCache<BaseListDto<GetAttractionsDto>>;