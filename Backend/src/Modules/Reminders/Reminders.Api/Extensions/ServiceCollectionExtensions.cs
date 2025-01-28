using BuildingBlocks.Api.Converters;
using BuildingBlocks.Application.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reminders.Application.Data;
using Reminders.Application.Extensions;
using Reminders.Infrastructure;

namespace Reminders.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRemindersModule
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiServices();
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);

        return services;
    }

    private static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ReminderIdConverter).Assembly);
        
        services.AddScoped<IRemindersService, RemindersService>();

        return services;
    }

    public static IEndpointRouteBuilder UseRemindersModule(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/Reminders/Test", () => "Reminders.Api Test -> Ok!");

        return endpoints;
    }
}
