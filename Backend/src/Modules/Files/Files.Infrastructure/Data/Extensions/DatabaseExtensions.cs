namespace Files.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedFilesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<FilesDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(context);
    }

    private static async Task SeedAsync(FilesDbContext context)
    {
        if (await context.FileAssets.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.FileAssets);
        await context.SaveChangesAsync();
    }
}
