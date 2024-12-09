using Attractions.Application.Caches.AttractionCaches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Caches.ImageCaches
{
    public class CleanImagesCacheService : ICleanImagesCacheService
    {
        private readonly ImageMemoryCache _imageMemoryCache;

        private readonly ImagesListMemoryCache _imagesListMemoryCache;

        private readonly ImagesCountMemoryCache _imagesCountMemoryCache;

        public CleanImagesCacheService(
            ImageMemoryCache imageMemoryCache,
            ImagesListMemoryCache imagesListMemoryCache,
            ImagesCountMemoryCache imagesCountMemoryCache)
        {
            _imageMemoryCache = imageMemoryCache;
            _imagesListMemoryCache = imagesListMemoryCache;
            _imagesCountMemoryCache = imagesCountMemoryCache;
        }

        public void ClearAllCaches()
        {
            _imageMemoryCache.Clear();
            _imagesListMemoryCache.Clear();
            _imagesCountMemoryCache.Clear();
        }

        public void ClearListCaches()
        {
            _imagesListMemoryCache.Clear();
            _imagesCountMemoryCache.Clear();
        }
    }
}
