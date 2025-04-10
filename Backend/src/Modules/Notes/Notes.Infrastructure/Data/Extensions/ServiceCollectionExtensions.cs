using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Notes.Application.Data.Abstractions;
using Notes.Infrastructure.Data.Repositories;

namespace Notes.Infrastructure.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Note-specific DbContext
        services.AddDbContext<NotesDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(connectionString);
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
        });

        // Register DbContext interface
        services.AddScoped<INotesDbContext, NotesDbContext>();
        services.AddScoped<INotesRepository, NotesRepository>();

        return services;
    }
}
