using Travel.Application.Caches.AttractionFeedback;

namespace Attractions.Application.Caches.AttractionFeedback
{
    internal class CleanAttractionFeedbacksCacheService : ICleanAttractionFeedbacksCacheService
    {
        private readonly AttractionFeedbackMemoryCache _attractionFeedbackMemoryCache;

        private readonly AttractionFeedbacksListMemoryCache _attractionFeedbacksListMemoryCache;

        private readonly AttractionFeedbacksCountMemoryCache _attractionFeedbacksCountMemoryCache;

        public CleanAttractionFeedbacksCacheService(
            AttractionFeedbackMemoryCache attractionFeedbackMemoryCache,
            AttractionFeedbacksListMemoryCache attractionFeedbacksListMemoryCache,
            AttractionFeedbacksCountMemoryCache attractionFeedbacksCountMemoryCache)
        {
            _attractionFeedbackMemoryCache = attractionFeedbackMemoryCache;
            _attractionFeedbacksListMemoryCache = attractionFeedbacksListMemoryCache;
            _attractionFeedbacksCountMemoryCache = attractionFeedbacksCountMemoryCache;
        }

        public void ClearAllCaches()
        {
            _attractionFeedbackMemoryCache.Clear();
             _attractionFeedbacksListMemoryCache.Clear();
            _attractionFeedbacksCountMemoryCache.Clear();
        }

        public void ClearListCaches()
        {
            _attractionFeedbacksListMemoryCache.Clear();
            _attractionFeedbacksCountMemoryCache.Clear();
        }
    }
}
