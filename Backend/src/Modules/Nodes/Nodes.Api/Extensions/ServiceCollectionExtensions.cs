using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Infrastructure;

namespace Nodes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNodesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        
        services.AddApplicationLayer();
        services.AddNodesInfrastructureServices(configuration);
        
        return services;
    }

    public static IEndpointRouteBuilder MapNodesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Nodes", () => "Hello World! This is Timelines Nodes module.");
        endpoints.MapCarter();
        
        return endpoints;
    }
}
