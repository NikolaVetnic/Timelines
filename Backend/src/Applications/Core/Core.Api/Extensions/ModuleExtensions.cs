using Auth.Api.Extensions;
using BugTracking.Api.Extensions;
using Files.Api.Extensions;
using Nodes.Api.Extensions;
using Notes.Api.Extensions;
using Reminders.Api.Extensions;
using Timelines.Api.Extensions;

namespace Core.Api.Extensions;

public static class ModuleExtensions
{
    public static IServiceCollection AddModules(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthModule(configuration)
            .AddBugTrackingModule(configuration)
            .AddFilesModule(configuration)
            .AddNodesModule(configuration)
            .AddNotesModule(configuration)
            .AddRemindersModule(configuration)
            .AddTimelinesModule(configuration);

        return services;
    }

    public static IEndpointRouteBuilder UseModules(this IEndpointRouteBuilder endpoints)
    {
        endpoints
            .UseAuthModule()
            .UseBugTrackingModule()
            .UseFilesModule()
            .UseNodesModule()
            .UseNotesModule()
            .UseRemindersModule()
            .UseTimelinesModule();

        return endpoints;
    }
}
