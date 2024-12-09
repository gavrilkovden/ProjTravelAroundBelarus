using Core.Application.BaseRealizations;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attractions.Application.Caches.AttractionFeedback
{
    public class AttractionFeedbacksCountMemoryCache : BaseCache<int>;
}
