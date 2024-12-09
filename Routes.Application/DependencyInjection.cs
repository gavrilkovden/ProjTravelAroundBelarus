using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Routes.Application.Caches;
using System.Reflection;

namespace Routes.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRoutesApplication(this IServiceCollection services)
        {
            return services
                .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true)
                .AddSingleton<RouteMemoryCache>()
                .AddSingleton<RoutesListMemoryCache>()
                .AddSingleton<RoutesCountMemoryCache>()
                .AddTransient<ICleanRoutesCacheService, CleanRoutesCacheService>();
        }
    }
}
