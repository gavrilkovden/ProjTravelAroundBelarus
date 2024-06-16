using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Abstractions
{
    public interface IBaseRedisCache<TItem>
    {
        Task SetAsync<TRequest>(TRequest request, string secondKey, TItem item);
        Task SetAsync<TRequest>(TRequest request, TItem item);
        Task<(bool, TItem?)> TryGetValueAsync<TRequest>(TRequest request);
        Task<(bool, TItem?)> TryGetValueAsync<TRequest>(TRequest request, string secondKey);
        Task DeleteItemAsync<TRequest>(TRequest request);
        Task DeleteItemAsync<TRequest>(TRequest request, string secondKey);
        Task ClearAsync(string prefix);
    }
}
