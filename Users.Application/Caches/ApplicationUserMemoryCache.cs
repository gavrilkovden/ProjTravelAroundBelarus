using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Users.Application.Dtos;

namespace Users.Application.Caches;

public class ApplicationUserMemoryCache : BaseCache<GetUserDto>;