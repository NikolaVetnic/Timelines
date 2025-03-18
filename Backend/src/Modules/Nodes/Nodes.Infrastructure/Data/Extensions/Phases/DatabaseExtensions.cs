namespace Nodes.Infrastructure.Data.Extensions.Phases;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedPhasesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<PhasesDbContext>();

        // Apply migrations
        await context.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(context);
    }

    private static async Task SeedAsync(PhasesDbContext context)
    {
        if (await context.Phases.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Phases);
        await context.SaveChangesAsync();
    }
}
