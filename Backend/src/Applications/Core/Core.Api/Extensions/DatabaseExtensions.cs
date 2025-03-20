using Files.Infrastructure.Data.Extensions;
using Nodes.Infrastructure.Data.Extensions.Nodes;
using Nodes.Infrastructure.Data.Extensions.Phases;
using Notes.Infrastructure.Data.Extensions;
using Reminders.Infrastructure.Data.Extensions;
using Timelines.Infrastructure.Data.Extensions;

namespace Core.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedAllModulesAsync(this IServiceProvider services, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment()) 
            return;
        
        await services.MigrateAndSeedFilesDatabaseAsync();
        await services.MigrateAndSeedNodesDatabaseAsync();
        await services.MigrateAndSeedNotesDatabaseAsync();
        await services.MigrateAndSeedRemindersDatabaseAsync();
        await services.MigrateAndSeedTimelinesDatabaseAsync();
        await services.MigrateAndSeedPhasesDatabaseAsync();
    }
}
