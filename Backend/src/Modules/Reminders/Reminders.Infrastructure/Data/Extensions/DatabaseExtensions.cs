namespace Reminders.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedRemindersDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var remindersDbContext = scopedProvider.GetRequiredService<RemindersDbContext>();

        // Apply migrations
        await remindersDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(remindersDbContext);
    }

    private static async Task SeedAsync(RemindersDbContext context)
    {
        if (await context.Reminders.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Reminders);
        await context.SaveChangesAsync();
    }

    public static async Task MigrateRemindersDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var remindersDbContext = scopedProvider.GetRequiredService<RemindersDbContext>();

        await remindersDbContext.Database.MigrateAsync();
    }
}
