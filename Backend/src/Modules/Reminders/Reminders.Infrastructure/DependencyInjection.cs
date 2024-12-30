using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Reminders.Application.Data;
using Reminders.Infrastructure.Data;
using Reminders.Infrastructure.Data.Interceptors;

namespace Reminders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Add Reminder-specific DbContext
        services.AddDbContext<RemindersDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<IRemindersDbContext, RemindersDbContext>();

        return services;
    }
}
