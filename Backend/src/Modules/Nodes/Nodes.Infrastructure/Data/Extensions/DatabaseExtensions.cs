using BuildingBlocks.Application.Data;

namespace Nodes.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedNodesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var nodesDbContext = scopedProvider.GetRequiredService<NodesDbContext>();
        var timelineService = scopedProvider.GetRequiredService<ITimelinesService>();

        // Apply migrations
        await nodesDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(nodesDbContext, timelineService);
    }

    private static async Task SeedAsync(NodesDbContext context, ITimelinesService timelineService)
    {
        if (await context.Nodes.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Nodes);
        await context.SaveChangesAsync();

        foreach (var initialTimeline in InitialData.Nodes)
            await timelineService.AddNode(initialTimeline.TimelineId, initialTimeline.Id, CancellationToken.None);
    }
}
