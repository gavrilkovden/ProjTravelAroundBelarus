namespace Tours.Application.Caches.TourCaches
{

    internal class CleanToursCacheService : ICleanToursCacheService
    {
        private readonly TourMemoryCache _tourMemoryCache;

        private readonly ToursListMemoryCache _toursListMemoryCache;

        private readonly ToursCountMemoryCache _toursCountMemoryCache;

        public CleanToursCacheService(
            TourMemoryCache tourMemoryCache,
            ToursListMemoryCache toursListMemoryCache,
            ToursCountMemoryCache toursCountMemoryCache)
        {
            _tourMemoryCache = tourMemoryCache;
            _toursListMemoryCache = toursListMemoryCache;
            _toursCountMemoryCache = toursCountMemoryCache;
        }

        public void ClearAllCaches()
        {
             _tourMemoryCache.Clear();
             _toursListMemoryCache.Clear();
             _toursCountMemoryCache.Clear();
        }

        public void ClearListCaches()
        {
            _toursListMemoryCache.Clear();
            _toursCountMemoryCache.Clear();
        }
    }
}
