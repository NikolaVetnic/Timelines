namespace Timelines.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedTimelinesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<TimelinesDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(context);
    }

    private static async Task SeedAsync(TimelinesDbContext context)
    {
        if (await context.Timelines.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Timelines);
        await context.SaveChangesAsync();
    }
}
