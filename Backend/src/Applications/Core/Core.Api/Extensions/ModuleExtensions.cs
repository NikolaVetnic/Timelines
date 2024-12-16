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
        services.AddFilesModule();
        services.AddNodesModule(configuration);
        services.AddNotesModule();
        services.AddRemindersModule();
        services.AddTimelinesModule();
        return services;
    }

    public static IEndpointRouteBuilder UseModules(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapFilesModuleEndpoints();
        endpoints.UseNodesModule();
        endpoints.MapNotesModuleEndpoints();
        endpoints.MapRemindersModuleEndpoints();
        endpoints.MapTimelinesModuleEndpoints();
        return endpoints;
    }
}