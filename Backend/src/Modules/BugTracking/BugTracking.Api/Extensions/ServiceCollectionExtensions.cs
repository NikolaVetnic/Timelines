using BugTracking.Application.Extensions;
using BugTracking.Infrastructure.Data.Extensions;
using BuildingBlocks.Api.Converters;
using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTracking.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBugTrackingModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(BugReportIdConverter).Assembly);

        return services;
    }

    public static IEndpointRouteBuilder UseBugTrackingModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/BugTracking/Test", () => "BugTracking.Api Test -> Ok!");

        return endpoints;
    }
}