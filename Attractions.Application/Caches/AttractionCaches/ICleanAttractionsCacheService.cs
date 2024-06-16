namespace Attractions.Application.Caches.AttractionCaches;

public interface ICleanAttractionsCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}