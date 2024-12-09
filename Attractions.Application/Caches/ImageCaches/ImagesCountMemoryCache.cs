using Core.Application.BaseRealizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Caches.ImageCaches
{
    public class ImagesCountMemoryCache : BaseCache<int>
    {
        protected override int AbsoluteExpiration => 10; // minutes
        protected override int SlidingExpiration => 5; // minutes
    }
}
