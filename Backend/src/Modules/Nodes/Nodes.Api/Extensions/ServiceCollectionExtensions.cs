using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Extensions;
using Nodes.Infrastructure;

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
        return services;
    }

    public static IEndpointRouteBuilder UseNodesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Nodes/Test", () => "Nodes.Api Test -> Ok!");
        
        return endpoints;
    }
}
