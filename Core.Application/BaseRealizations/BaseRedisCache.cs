//using Core.Application.Abstractions;
//using Microsoft.Extensions.Caching.Distributed;
//using StackExchange.Redis;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Core.Application.BaseRealizations
//{
//    public class BaseRedisCache<TItem> : IBaseRedisCache<TItem>
//    {
//        private readonly IConnectionMultiplexer _connectionMultiplexer;
//        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
//        private readonly string _instanceName;

//        public BaseRedisCache(IConnectionMultiplexer connectionMultiplexer, string instanceName)
//        {
//            _connectionMultiplexer = connectionMultiplexer;
//            _instanceName = instanceName;
//        }

//        private string CreateCacheKey<TRequest>(TRequest request)
//        {
//            return $"{_instanceName}:{JsonSerializer.Serialize(request)}";
//        }

//        private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
//        {
//            return $"{_instanceName}:{JsonSerializer.Serialize(request)}_{secondKey}";
//        }

//        public async Task SetAsync<TRequest>(TRequest request, string secondKey, TItem item)
//        {
//            var cacheKey = CreateCacheKey(request, secondKey);
//            var database = _connectionMultiplexer.GetDatabase();
//            var jsonData = JsonSerializer.Serialize(item, _jsonOptions);
//            await database.StringSetAsync(cacheKey, jsonData, TimeSpan.FromMinutes(10));
//        }

//        public async Task SetAsync<TRequest>(TRequest request, TItem item)
//        {
//            var cacheKey = CreateCacheKey(request);
//            var database = _connectionMultiplexer.GetDatabase();
//            var jsonData = JsonSerializer.Serialize(item, _jsonOptions);

//            // Log the cache key and data being set
//            Console.WriteLine($"Setting cache with key: {cacheKey}, data: {jsonData}");

//            await database.StringSetAsync(cacheKey, jsonData, TimeSpan.FromMinutes(10));
//        }

//        public async Task<(bool, TItem?)> TryGetValueAsync<TRequest>(TRequest request)
//        {
//            var cacheKey = CreateCacheKey(request);
//            var database = _connectionMultiplexer.GetDatabase();
//            var jsonData = await database.StringGetAsync(cacheKey);

//            if (!jsonData.IsNull)
//            {
//                var item = JsonSerializer.Deserialize<TItem>(jsonData, _jsonOptions);
//                return (true, item);
//            }

//            return (false, default);
//        }

//        public async Task<(bool, TItem?)> TryGetValueAsync<TRequest>(TRequest request, string secondKey)
//        {
//            var cacheKey = CreateCacheKey(request, secondKey);
//            var database = _connectionMultiplexer.GetDatabase();
//            var jsonData = await database.StringGetAsync(cacheKey);

//            if (!jsonData.IsNull)
//            {
//                var item = JsonSerializer.Deserialize<TItem>(jsonData, _jsonOptions);
//                return (true, item);
//            }

//            return (false, default);
//        }

//        public async Task DeleteItemAsync<TRequest>(TRequest request)
//        {
//            var cacheKey = CreateCacheKey(request);
//            var database = _connectionMultiplexer.GetDatabase();
//            await database.KeyDeleteAsync(cacheKey);
//        }

//        public async Task DeleteItemAsync<TRequest>(TRequest request, string secondKey)
//        {
//            var cacheKey = CreateCacheKey(request, secondKey);
//            var database = _connectionMultiplexer.GetDatabase();
//            await database.KeyDeleteAsync(cacheKey);
//        }

//        public async Task ClearAsync(string prefix)
//        {
//            var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
//            var keys = server.Keys(pattern: $"{_instanceName}:{prefix}*").ToArray();

//            // Log the keys found
//            Console.WriteLine($"Keys found for pattern {_instanceName}:{prefix}*: {string.Join(", ", keys)}");

//            var database = _connectionMultiplexer.GetDatabase();
//            foreach (var key in keys)
//            {
//                // Log each key being deleted
//                Console.WriteLine($"Deleting key: {key}");
//                await database.KeyDeleteAsync(key);
//            }
//        }
//    }
//}