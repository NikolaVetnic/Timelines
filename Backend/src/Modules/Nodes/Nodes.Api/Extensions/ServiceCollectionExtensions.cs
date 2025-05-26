using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Api.Controllers.Nodes;
using Nodes.Application.Data;
using Nodes.Application.Extensions;
using Nodes.Infrastructure.Data.Extensions;

namespace Nodes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNodesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(NodeIdConverter).Assembly);

        services.AddControllers().AddApplicationPart(typeof(NodesController).Assembly);

        services.AddScoped<INodesService, NodesService>();

        return services;
    }

    public static IEndpointRouteBuilder UseNodesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Nodes/Test", () => "Nodes.Api Test -> Ok!");

        return endpoints;
    }
}
