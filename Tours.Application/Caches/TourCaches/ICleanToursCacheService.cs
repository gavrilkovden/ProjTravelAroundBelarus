using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Application.Caches.TourCaches
{
    public interface ICleanToursCacheService
    {
        public void ClearAllCaches();
        public void ClearListCaches();
    }
}
