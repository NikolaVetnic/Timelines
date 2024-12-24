using Nodes.Infrastructure.Data.Extensions;
using Reminders.Infrastructure.Data.Extensions;

namespace Core.Api.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAndSeedAllModulesAsync(this IServiceProvider services)
    {
        await services.MigrateAndSeedNodesDatabaseAsync();
        await services.MigrateAndSeedRemindersDatabaseAsync();
    }
}
