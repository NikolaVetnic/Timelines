﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Files.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
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