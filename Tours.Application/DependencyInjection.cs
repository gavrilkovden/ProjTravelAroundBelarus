using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tours.Application.Caches.TourCaches;
using Tours.Application.Caches.TourFeedbackCaches;

namespace Tours.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddToursApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddTransient<ICleanToursCacheService, CleanToursCacheService>()
                .AddSingleton<TourMemoryCache>()
                .AddSingleton<ToursListMemoryCache>()
                .AddSingleton<ToursCountMemoryCache>()
                .AddTransient<ICleanTourFeedbacksCacheService, CleanTourFeedbacksCacheService>()
                .AddSingleton<TourFeedbackMemoryCache>()
                .AddSingleton<TourFeedbacksListMemoryCache>()
                .AddSingleton<TourFeedbacksCountMemoryCache>();
        }
    }
}
