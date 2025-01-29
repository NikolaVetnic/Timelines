using BuildingBlocks.Application.Data;

namespace Reminders.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedRemindersDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var remindersDbContext = scopedProvider.GetRequiredService<RemindersDbContext>();
        var nodesService = scopedProvider.GetRequiredService<INodesService>();

        // Apply migrations
        await remindersDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(remindersDbContext, nodesService);
    }

    private static async Task SeedAsync(RemindersDbContext context, INodesService nodesService)
    {
        if (await context.Reminders.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Reminders);
        await context.SaveChangesAsync();

        foreach (var initialReminder in InitialData.Reminders)
            await nodesService.AddReminder(initialReminder.NodeId, initialReminder.Id, CancellationToken.None);
    }
}
