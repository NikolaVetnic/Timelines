namespace Notes.Infrastructure.Data.Extensions;
public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedNotesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var notesDbContext = scopedProvider.GetRequiredService<NotesDbContext>();

        // Apply migrations
        await notesDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(notesDbContext);
    }

    private static async Task SeedAsync(NotesDbContext context)
    {
        if (await context.Notes.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Notes);
        await context.SaveChangesAsync();
    }
}
