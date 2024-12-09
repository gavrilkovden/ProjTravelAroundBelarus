namespace Attractions.Application.Caches.AttractionCaches;

public class CleanAttractionsCacheService : ICleanAttractionsCacheService
{
    private readonly AttractionMemoryCache _attractionMemoryCache;

    private readonly AttractionsListMemoryCache _attractionsListMemoryCache;

    private readonly AttractionsCountMemoryCache _attractionsCountMemoryCache;

    public CleanAttractionsCacheService(
        AttractionMemoryCache attractionMemoryCache,
        AttractionsListMemoryCache attractionsListMemoryCache,
        AttractionsCountMemoryCache atractionsCountMemoryCache)
    {
        _attractionMemoryCache = attractionMemoryCache;
        _attractionsListMemoryCache = attractionsListMemoryCache;
        _attractionsCountMemoryCache = atractionsCountMemoryCache;
    }

    public void ClearAllCaches()
    {
        _attractionMemoryCache.Clear();
         _attractionsListMemoryCache.Clear();
        _attractionsCountMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _attractionsListMemoryCache.Clear();
        _attractionsCountMemoryCache.Clear();
    }
}
