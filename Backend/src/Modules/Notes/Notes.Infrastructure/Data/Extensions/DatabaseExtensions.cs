namespace Notes.Infrastructure.Data.Extensions;
public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedNodesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<NotesDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(context);
    }

    private static async Task SeedAsync(NotesDbContext context)
    {
        if (await context.Notes.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Notes);
        await context.SaveChangesAsync();
    }
}
