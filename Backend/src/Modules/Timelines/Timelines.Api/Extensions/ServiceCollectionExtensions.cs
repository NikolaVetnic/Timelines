using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Timelines.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTimelinesModule(this IServiceCollection services)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapTimelinesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Timelines", () => "Hello World! This is Timelines Timelines module.");
        return endpoints;
    }
}
