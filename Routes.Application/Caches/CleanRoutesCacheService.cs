namespace Routes.Application.Caches
{
    internal class CleanRoutesCacheService : ICleanRoutesCacheService
    {
        private readonly RouteMemoryCache _routeMemoryCache;

        private readonly RoutesListMemoryCache _routesListMemoryCache;

        private readonly RoutesCountMemoryCache _routesCountMemoryCache;

        public CleanRoutesCacheService(
            RouteMemoryCache routeMemoryCache,
            RoutesListMemoryCache routesListMemoryCache,
            RoutesCountMemoryCache routesCountMemoryCache)
        {
            _routeMemoryCache = routeMemoryCache;
            _routesListMemoryCache = routesListMemoryCache;
            _routesCountMemoryCache = routesCountMemoryCache;
        }

        public void ClearAllCaches()
        {
             _routeMemoryCache.Clear();
             _routesListMemoryCache.Clear();
            _routesCountMemoryCache.Clear();
        }

        public void ClearListCaches()
        {
            _routesListMemoryCache.Clear();
            _routesCountMemoryCache.Clear();
        }
    }
}
