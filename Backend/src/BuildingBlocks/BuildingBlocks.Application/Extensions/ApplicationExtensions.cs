using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddMediatRFromAssembly<T>(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(T).Assembly));
        return services;
    }
}