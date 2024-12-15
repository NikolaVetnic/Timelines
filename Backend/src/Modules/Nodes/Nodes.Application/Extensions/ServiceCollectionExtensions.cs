using System.Reflection;
using BuildingBlocks.Application.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Nodes.Application.Nodes.Commands.CreateNode;

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
        
        services.AddMediatRFromAssembly<CreateNodeCommand>();
        
        return services;
    }
}