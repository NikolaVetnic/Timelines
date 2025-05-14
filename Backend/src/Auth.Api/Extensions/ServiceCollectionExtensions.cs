using Auth.Api.Controllers;
using Auth.Application.Extensions;
using Auth.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddApiServices()
            .AddApplicationServices()
            .AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(AuthController).Assembly);

        return services;
    }

    public static IEndpointRouteBuilder UseAuthModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Auth/Test", () => "Auth.Api Test -> Ok!");

        return endpoints;
    }
}
