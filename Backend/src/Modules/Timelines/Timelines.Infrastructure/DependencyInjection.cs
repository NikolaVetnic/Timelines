﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Timelines.Application.Data;
using Timelines.Infrastructure.Data;
using Timelines.Infrastructure.Data.Interceptors;

namespace Timelines.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        // Add Timeline-specific DbContext
        services.AddDbContext<TimelinesDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<ITimelinesDbContext, TimelinesDbContext>();

        return services;
    }
}
