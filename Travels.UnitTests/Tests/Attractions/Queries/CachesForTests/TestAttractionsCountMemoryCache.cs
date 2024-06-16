using Core.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Travel.UnitTests.Tests.Attractions.Queries.CachesForTests
{
    public class TestBaseCache<TItem> : IBaseCache<TItem>
    {
        private readonly Dictionary<string, TItem> _cacheDictionary = new();

        protected virtual int AbsoluteExpiration => 10;
        protected virtual int SlidingExpiration => 5;

        private string CreateCacheKey<TRequest>(TRequest request)
        {
            return JsonSerializer.Serialize(request);
        }

        private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
        {
            return $"{JsonSerializer.Serialize(request)}_{secondKey}";
        }

        public void Set<TRequest>(TRequest request, string secondKey, TItem item, int size)
        {
            _cacheDictionary[CreateCacheKey(request, secondKey)] = item;
        }

        public void Set<TRequest>(TRequest request, TItem item, int size)
        {
            _cacheDictionary[CreateCacheKey(request)] = item;
        }

        public bool TryGetValue<TRequest>(TRequest request, out TItem? item)
        {
            var key = CreateCacheKey(request);
            if (_cacheDictionary.TryGetValue(key, out TItem cachedItem))
            {
                item = cachedItem;
                return true;
            }
            item = default;
            return false;
        }

        public bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item)
        {
            var key = CreateCacheKey(request, secondKey);
            if (_cacheDictionary.TryGetValue(key, out TItem cachedItem))
            {
                item = cachedItem;
                return true;
            }
            item = default;
            return false;
        }

        public void DeleteItem<TRequest>(TRequest request)
        {
            _cacheDictionary.Remove(CreateCacheKey(request));
        }

        public void DeleteItem<TRequest>(TRequest request, string secondKey)
        {
            _cacheDictionary.Remove(CreateCacheKey(request, secondKey));
        }

        public void Clear()
        {
            _cacheDictionary.Clear();
        }
    }

}