namespace Nodes.Infrastructure.Data.Extensions.Phases;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedPhasesDatabaseAsync(this IServiceProvider services)
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
        if (await context.Phas.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Phases);
        await context.SaveChangesAsync();
    }

    public static async Task MigratePhasesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var nodesDbContext = scopedProvider.GetRequiredService<PhasesDbContext>();

        await nodesDbContext.Database.MigrateAsync();
    }
}
