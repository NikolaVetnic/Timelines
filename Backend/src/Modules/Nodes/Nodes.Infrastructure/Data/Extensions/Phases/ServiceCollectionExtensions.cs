using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Nodes.Application.Data.Abstractions;
using Nodes.Application.Data.Abstractions.Phases;
using Nodes.Infrastructure.Data.Repositories;

namespace Nodes.Infrastructure.Data.Extensions.Phases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Node-specific DbContext
        services.AddDbContext<NodesDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<IPhasesDbContext, PhasesDbContext>();
        services.AddScoped<IPhasesRepository, PhasesRepository>();

        return services;
    }
}
