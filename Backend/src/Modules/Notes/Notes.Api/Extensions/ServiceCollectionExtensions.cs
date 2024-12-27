using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Extensions;
using Notes.Infrastructure;

namespace Notes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    public static IEndpointRouteBuilder UseNotesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Notes", () => "Notes.Api Test -> Ok!");
        return endpoints;
    }
}
