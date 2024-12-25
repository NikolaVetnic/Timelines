using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reminders.Application.Extensions;
using Reminders.Infrastructure;

namespace Reminders.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRemindersModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // services.AddExceptionHandler<CustomExceptionHandler>();
        // services.AddHealthChecks()...

        return services;
    }

    public static IEndpointRouteBuilder UseRemindersModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Reminders/Test", () => "Reminders.Api Test -> Ok!");

        // app.UseExceptionHandler(_ => { });
        // app.UseHealthChecks...

        return endpoints;
    }
}
