﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tours.Application.Caches.TourFeedbackCaches
{
    public interface ICleanTourFeedbacksCacheService
    {
        public  void ClearAllCaches();
        public  void ClearListCaches();
    }
}
