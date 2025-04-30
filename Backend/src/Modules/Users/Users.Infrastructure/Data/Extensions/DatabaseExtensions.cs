using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateUsersDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<UsersDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();
    }
}
