using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Nodes.Application.Data;
using Nodes.Infrastructure.Data;

namespace Nodes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddNodesInfrastructureServices
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

        return services;
    }
}
