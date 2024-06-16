namespace Tours.Application.Caches.TourFeedbackCaches
{
    internal class CleanTourFeedbacksCacheService : ICleanTourFeedbacksCacheService
    {
        private readonly TourFeedbackMemoryCache _tourFeedbackMemoryCache;

        private readonly TourFeedbacksListMemoryCache _tourFeedbacksListMemoryCache;

        private readonly TourFeedbacksCountMemoryCache _tourFeedbacksCountMemoryCache;

        public CleanTourFeedbacksCacheService(
            TourFeedbackMemoryCache tourFeedbackMemoryCache,
            TourFeedbacksListMemoryCache tourFeedbacksListMemoryCache,
            TourFeedbacksCountMemoryCache tourFeedbacksCountMemoryCache)
        {
            _tourFeedbackMemoryCache = tourFeedbackMemoryCache;
            _tourFeedbacksListMemoryCache = tourFeedbacksListMemoryCache;
            _tourFeedbacksCountMemoryCache = tourFeedbacksCountMemoryCache;
        }

        public  void ClearAllCaches()
        {
            _tourFeedbackMemoryCache.Clear();
             _tourFeedbacksListMemoryCache.Clear();
            _tourFeedbacksCountMemoryCache.Clear();
        }

        public void ClearListCaches()
        {
             _tourFeedbacksListMemoryCache.Clear();
            _tourFeedbacksCountMemoryCache.Clear();
        }   
    }
}
