using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Data;
using Nodes.Application.Extensions;
using Nodes.Infrastructure.Data.Extensions.Phases;

namespace Nodes.Api.Extensions.Phases;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPhasesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(PhaseIdConverter).Assembly);

        services.AddScoped<IPhasesService, PhasesService>();

        return services;
    }

    public static IEndpointRouteBuilder UsePhasesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Phases/Test", () => "Phases.Api Test -> Ok!");

        return endpoints;
    }
}
