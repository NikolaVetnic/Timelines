namespace Nodes.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedNodesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var nodesDbContext = scopedProvider.GetRequiredService<NodesDbContext>();

        // Apply migrations
        await nodesDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(nodesDbContext);
    }

    private static async Task SeedAsync(NodesDbContext context)
    {
        if (await context.Nodes.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Nodes);
        await context.SaveChangesAsync();
    }
}
