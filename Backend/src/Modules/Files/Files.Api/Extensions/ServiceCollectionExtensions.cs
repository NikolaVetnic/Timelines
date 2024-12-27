using Files.Application.Extensions;
using Files.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilesModule
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

    public static IEndpointRouteBuilder UseFilesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Files/Test", () => "Files.Api Test -> Ok!");

        // app.UseExceptionHandler(_ => { });
        // app.UseHealthChecks...

        return endpoints;
    }
}
