using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Users.Application.Caches;


public class ApplicationUsersCountMemoryCache : BaseCache<int>;