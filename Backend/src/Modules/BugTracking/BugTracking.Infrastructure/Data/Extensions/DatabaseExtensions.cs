using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BugTracking.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateBugTrackingDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<BugTrackingDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();
    }
}