using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Files.Api.Controllers.Files;
using Files.Application.Data;
using Files.Application.Extensions;
using Files.Infrastructure.Data.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Files.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(FileIdConverter).Assembly);

        services.AddControllers().AddApplicationPart(typeof(FilesController).Assembly);

        services.AddScoped<IFilesService, FilesService>();

        return services;
    }

    public static IEndpointRouteBuilder UseFilesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Files/Test", () => "Files.Api Test -> Ok!");

        return endpoints;
    }
}
