using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Application.Caches.AttractionFeedback
{
    public interface ICleanAttractionFeedbacksCacheService
    {
        public void ClearAllCaches();
        public void ClearListCaches();
    }
}
