using BugTracking.Infrastructure.Data.Extensions;
using Files.Infrastructure.Data.Extensions;
using Nodes.Infrastructure.Data.Extensions.Nodes;
using Nodes.Infrastructure.Data.Extensions.Phases;
using Notes.Infrastructure.Data.Extensions;
using Reminders.Infrastructure.Data.Extensions;
using Timelines.Infrastructure.Data.Extensions;

namespace Core.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task SetupDatabaseAsync(this IServiceProvider services, IWebHostEnvironment env)
    {
        switch (env.EnvironmentName)
        {
            case "Development":
            case "Production": // Temporary solution as we want test clients to have some templates to go off of
                await services.MigrateAndSeedAllModulesAsync();
                break;
            default:
                await services.MigrateAllModulesAsync();
                break;
        }
    }

    private static async Task MigrateAllModulesAsync(this IServiceProvider services)
    {
        await services.MigrateBugTrackingDatabaseAsync();
        await services.MigrateFilesDatabaseAsync();
        await services.MigrateNodesDatabaseAsync();
        await services.MigrateNotesDatabaseAsync();
        await services.MigratePhasesDatabaseAsync();
        await services.MigrateRemindersDatabaseAsync();
        await services.MigrateTimelinesDatabaseAsync();
    }

    private static async Task MigrateAndSeedAllModulesAsync(this IServiceProvider services)
    {
        await services.MigrateBugTrackingDatabaseAsync();
        await services.MigrateAndSeedFilesDatabaseAsync();
        await services.MigrateAndSeedNodesDatabaseAsync();
        await services.MigrateAndSeedNotesDatabaseAsync();
        await services.MigrateAndSeedPhasesDatabaseAsync();
        await services.MigrateAndSeedRemindersDatabaseAsync();
        await services.MigrateAndSeedTimelinesDatabaseAsync();
    }
}