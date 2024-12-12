using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Reminders.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRemindersModule(this IServiceCollection services)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapRemindersModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Reminders", () => "Hello World! This is Timelines Reminders module.");
        return endpoints;
    }
}
