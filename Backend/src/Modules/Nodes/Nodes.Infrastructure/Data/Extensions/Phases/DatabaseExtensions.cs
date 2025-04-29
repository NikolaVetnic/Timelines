namespace Nodes.Infrastructure.Data.Extensions.Phases;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedPhasesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var phasesDbContext = scopedProvider.GetRequiredService<PhasesDbContext>();

        // Apply migrations
        await phasesDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(phasesDbContext);
    }

    private static async Task SeedAsync(PhasesDbContext context)
    {
        if (await context.Phases.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Phases);
        await context.SaveChangesAsync();
    }

    public static async Task MigratePhasesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var phasesDbContext = scopedProvider.GetRequiredService<PhasesDbContext>();

        await phasesDbContext.Database.MigrateAsync();
    }
}
