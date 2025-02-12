using BuildingBlocks.Application.Data;

namespace Notes.Infrastructure.Data.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedNotesDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var notesDbContext = scopedProvider.GetRequiredService<NotesDbContext>();
        var nodesService = scopedProvider.GetRequiredService<INodesService>();

        // Apply migrations
        await notesDbContext.Database.MigrateAsync();

        // Seed initial data if necessary
        await SeedAsync(notesDbContext, nodesService);
    }

    private static async Task SeedAsync(NotesDbContext context, INodesService nodesService)
    {
        if (await context.Notes.AnyAsync())
            return;

        await context.AddRangeAsync(InitialData.Notes);
        await context.SaveChangesAsync();

        foreach (var initialNote in InitialData.Notes)
            await nodesService.AddNote(initialNote.NodeId, initialNote.Id, CancellationToken.None);
    }
}
