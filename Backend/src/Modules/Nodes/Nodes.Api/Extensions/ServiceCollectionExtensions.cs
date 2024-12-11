using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Nodes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNodesModule(this IServiceCollection services)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapNodesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Nodes", NodesDomainTestClass.GetTestString);
        return endpoints;
    }
}