using System.Reflection;
using Auth.Application.Data;
using Auth.Application.Data.Abstractions;
using BuildingBlocks.Application.Behaviors;
using BuildingBlocks.Application.Data;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
