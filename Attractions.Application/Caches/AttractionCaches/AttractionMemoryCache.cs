using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Travel.Application.Dtos;

namespace Attractions.Application.Caches.AttractionCaches;


public class AttractionMemoryCache : BaseCache<GetAttractionDto>;