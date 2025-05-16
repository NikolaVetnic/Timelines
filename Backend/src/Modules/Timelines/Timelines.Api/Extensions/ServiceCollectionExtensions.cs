using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Timelines.Api.Controllers.Phases;
using Timelines.Api.Controllers.Timelines;
using Timelines.Application.Data;
using Timelines.Application.Extensions;
using Timelines.Infrastructure.Data.Extensions;

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
        TypeAdapterConfig.GlobalSettings.Scan(typeof(PhaseIdConverter).Assembly);

        services.AddControllers().AddApplicationPart(typeof(TimelinesController).Assembly);
        services.AddControllers().AddApplicationPart(typeof(PhasesController).Assembly);

        services.AddScoped<ITimelinesService, TimelinesService>();

        return services;
    }

    public static IEndpointRouteBuilder UseTimelinesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Timelines/Test", () => "Timelines.Api Test -> Ok!");

        return endpoints;
    }
}
