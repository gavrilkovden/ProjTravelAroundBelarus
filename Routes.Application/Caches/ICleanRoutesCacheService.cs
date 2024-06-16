using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routes.Application.Caches
{
    public interface ICleanRoutesCacheService
    {
        public void ClearAllCaches();
        public void ClearListCaches();
    }
}
