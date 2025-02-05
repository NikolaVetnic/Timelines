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
        services.AddFilesModule(configuration);
        services.AddNodesModule(configuration);
        services.AddNotesModule(configuration);
        services.AddRemindersModule(configuration);
        services.AddTimelinesModule(configuration);
        
        return services;
    }

    public static IEndpointRouteBuilder UseModules(this IEndpointRouteBuilder endpoints)
    {
        endpoints.UseFilesModule();
        endpoints.UseNodesModule();
        endpoints.UseNotesModule();
        endpoints.UseRemindersModule();
        endpoints.UseTimelinesModule();
        
        return endpoints;
    }
}
