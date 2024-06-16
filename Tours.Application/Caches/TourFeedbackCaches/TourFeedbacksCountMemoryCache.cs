using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Application.Caches.TourFeedbackCaches
{
    public class TourFeedbacksCountMemoryCache : BaseCache<int>;
}
