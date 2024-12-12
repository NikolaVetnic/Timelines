using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilesModule(this IServiceCollection services)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapFilesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Files", () => "Hello World! This is Timelines Files module.");
        return endpoints;
    }
}