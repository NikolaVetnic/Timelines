using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Reminders.Application.Data.Abstractions;
using Reminders.Infrastructure.Data.Repositories;

namespace Reminders.Infrastructure.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Reminder-specific DbContext
        services.AddDbContext<RemindersDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<IRemindersDbContext, RemindersDbContext>();
        services.AddScoped<IRemindersRepository, RemindersRepository>();

        return services;
    }
}
