using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Timelines.Application.Data.Abstractions;
using Timelines.Infrastructure.Data.Repositories;

namespace Timelines.Infrastructure.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Timeline-specific DbContext
        services.AddDbContext<TimelinesDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<ITimelinesDbContext, TimelinesDbContext>();
        services.AddScoped<ITimelinesRepository, TimelinesRepository>();

        return services;
    }
}
