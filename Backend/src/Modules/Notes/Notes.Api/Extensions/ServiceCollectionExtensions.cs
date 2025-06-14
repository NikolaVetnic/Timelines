using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Api.Controllers.Notes;
using Notes.Application.Data;
using Notes.Application.Extensions;
using Notes.Infrastructure.Data.Extensions;

namespace Notes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNotesModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(NoteIdConverter).Assembly);

        services.AddControllers().AddApplicationPart(typeof(NotesController).Assembly);

        services.AddScoped<INotesService, NotesService>();

        return services;
    }

    public static IEndpointRouteBuilder UseNotesModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Notes/Test", () => "Notes.Api Test -> Ok!");

        return endpoints;
    }
}
