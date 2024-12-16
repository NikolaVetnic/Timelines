using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nodes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            // config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        
        return services;
    }
}