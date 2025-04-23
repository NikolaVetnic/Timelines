using BuildingBlocks.Application.Data;

namespace Files.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedFilesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var fileDbContext = scopedProvider.GetRequiredService<FilesDbContext>();
        var nodesService = scopedProvider.GetRequiredService<INodesService>();

        // Apply migrations
        await fileDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(fileDbContext, nodesService);
    }

    private static async Task SeedAsync(FilesDbContext context, INodesService nodesService)
    {
        if (await context.FileAssets.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.FileAssets);
        await context.SaveChangesAsync();

        foreach (var initialFileAsset in InitialData.FileAssets)
            await nodesService.AddFileAsset(initialFileAsset.NodeId, initialFileAsset.Id, CancellationToken.None);
    }
}