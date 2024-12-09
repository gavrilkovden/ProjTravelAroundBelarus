using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Caches.ImageCaches
{
    public interface ICleanImagesCacheService
    {
        void ClearAllCaches();
        void ClearListCaches();
    }
}
