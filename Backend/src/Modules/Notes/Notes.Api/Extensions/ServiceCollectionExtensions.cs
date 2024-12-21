using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Extensions;
using Notes.Infrastructure;

namespace Notes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static void AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
    }

    public static IEndpointRouteBuilder MapNotesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Notes", () => "Hello World! This is Timelines Notes module.");
        return endpoints;
    }
}
