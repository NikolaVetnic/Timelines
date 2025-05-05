using BuildingBlocks.Api.Converters;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Extensions;
using Users.Infrastructure.Data.Extensions;

namespace Users.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsersModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(UserIdConverter).Assembly);

        return services;
    }

    public static IEndpointRouteBuilder UseUsersModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Users/Test", () => "Users.Api Test -> Ok!");

        return endpoints;
    }
}
