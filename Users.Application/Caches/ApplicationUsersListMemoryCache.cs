using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Users.Application.Dtos;

namespace Users.Application.Caches;

public class ApplicationUsersListMemoryCache : BaseCache<BaseListDto<GetUserDto>>;
