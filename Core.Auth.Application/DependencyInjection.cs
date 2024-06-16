using Core.Auth.Application.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Auth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreAuthServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
    }
}