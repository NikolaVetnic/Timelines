using Files.Infrastructure.Data.Extensions;
using Nodes.Infrastructure.Data.Extensions;
using Reminders.Infrastructure.Data.Extensions;
using Timelines.Infrastructure.Data.Extensions;

namespace Core.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedAllModulesAsync(this IServiceProvider services)
    {
        await services.MigrateAndSeedFilesDatabaseAsync();
        await services.MigrateAndSeedNodesDatabaseAsync();
        await services.MigrateAndSeedRemindersDatabaseAsync();
        await services.MigrateAndSeedTimelinesDatabaseAsync();
    }
}
