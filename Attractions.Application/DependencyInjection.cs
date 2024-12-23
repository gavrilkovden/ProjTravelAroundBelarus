﻿using Attractions.Application.Caches.AttractionCaches;
using Attractions.Application.Caches.AttractionFeedback;
using Attractions.Application.Caches.ImageCaches;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Travel.Application.Caches.AttractionFeedback;

namespace Attractions.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAttractionsApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddSingleton<AttractionMemoryCache>()
                .AddSingleton<AttractionsListMemoryCache>()
                .AddSingleton<AttractionsCountMemoryCache>()
                .AddTransient<ICleanAttractionsCacheService, CleanAttractionsCacheService>()
                .AddTransient<ICleanAttractionFeedbacksCacheService, CleanAttractionFeedbacksCacheService>()
                .AddSingleton<AttractionFeedbackMemoryCache>()
                .AddSingleton<AttractionFeedbacksListMemoryCache>()
                .AddSingleton<AttractionFeedbacksCountMemoryCache>()
                .AddTransient<ICleanImagesCacheService, CleanImagesCacheService>()
                .AddSingleton<ImageMemoryCache>()
                .AddSingleton<ImagesListMemoryCache>()
                .AddSingleton<ImagesCountMemoryCache>();
        }
    }
}
