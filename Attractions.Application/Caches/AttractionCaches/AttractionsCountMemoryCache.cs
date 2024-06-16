using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Attractions.Application.Caches.AttractionCaches;

public class AttractionsCountMemoryCache : BaseCache<int>
{
    protected override int AbsoluteExpiration => 10; // minutes
    protected override int SlidingExpiration => 5; // minutes
}