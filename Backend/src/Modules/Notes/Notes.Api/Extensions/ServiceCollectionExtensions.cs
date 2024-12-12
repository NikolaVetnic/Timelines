using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Notes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotesModule(this IServiceCollection services)
    {
        return services;
    }

    public static IEndpointRouteBuilder MapNotesModuleEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Notes", () => "Hello World! This is Timelines Notes module.");
        return endpoints;
    }
}
