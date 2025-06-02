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
        var isDbSeeded = false;

        if (!await context.Timelines.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.Timelines);
            isDbSeeded = true;
        }

        if (!await context.Phases.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.Phases);
            isDbSeeded = true;
        }

        if (!await context.PhysicalPersons.AnyAsync())
        {
            await context.AddRangeAsync(InitialData.PhysicalPersons);
            isDbSeeded = true;
        }

        if (isDbSeeded)
            await context.SaveChangesAsync();
    }

    public static async Task MigrateTimelinesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<TimelinesDbContext>();

        await context.Database.MigrateAsync();
    }
}
