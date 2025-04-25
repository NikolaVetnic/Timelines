namespace Files.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedFilesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var fileDbContext = scopedProvider.GetRequiredService<FilesDbContext>();

        // Apply migrations
        await fileDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(fileDbContext);
    }

    private static async Task SeedAsync(FilesDbContext context)
    {
        if (await context.FileAssets.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.FileAssets);
        await context.SaveChangesAsync();
    }
}
