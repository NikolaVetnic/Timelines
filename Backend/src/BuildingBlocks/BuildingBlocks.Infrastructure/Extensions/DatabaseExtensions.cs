using Nodes.Infrastructure.Data.Extensions;

namespace BuildingBlocks.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedAllModulesAsync(this IServiceProvider services)
    {
        await services.MigrateAndSeedNodesDatabaseAsync();
    }
}