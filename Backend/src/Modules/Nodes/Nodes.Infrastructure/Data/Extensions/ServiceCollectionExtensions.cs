using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Nodes.Application.Data.Abstractions;
using Nodes.Infrastructure.Data.Repositories;

namespace Nodes.Infrastructure.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Node-specific DbContext
        services.AddDbContext<NodesDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<INodesDbContext, NodesDbContext>();
        services.AddScoped<INodesRepository, NodesRepository>();

        return services;
    }
}
