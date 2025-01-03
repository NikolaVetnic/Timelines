using BuildingBlocks.Api.Converters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Application.Extensions;
using Timelines.Infrastructure;

namespace Timelines.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTimelinesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(TimelineIdConverter).Assembly);

        return services;
    }

    public static IEndpointRouteBuilder UseTimelinesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Timelines/Test", () => "Timelines.Api Test -> Ok!");

        return endpoints;
    }
}
