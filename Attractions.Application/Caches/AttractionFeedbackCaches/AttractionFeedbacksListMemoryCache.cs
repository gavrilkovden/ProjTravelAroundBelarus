using Core.Application.BaseRealizations;
using Core.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Application.Dtos;

namespace Attractions.Application.Caches.AttractionFeedback
{
    public class AttractionFeedbacksListMemoryCache : BaseCache<BaseListDto<GetFeedbackAttractionDto>>; 
    
}
