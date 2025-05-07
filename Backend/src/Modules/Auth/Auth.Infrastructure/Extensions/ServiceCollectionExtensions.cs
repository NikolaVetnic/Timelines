using Auth.Domain.Models;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;

namespace Auth.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddAuthDbContext(configuration)
            .AddOpenIddictServer()
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    private static IServiceCollection AddAuthDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Add Auth-specific DbContext
        services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseOpenIddict(); // Enables OpenIddict integration
        });

        return services;
    }

    private static IServiceCollection AddOpenIddictServer(this IServiceCollection services)
    {
        services.AddOpenIddict()
            .AddCore(options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<AuthDbContext>();
            })
            .AddServer(options =>
            {
                options.RegisterScopes(
                    OpenIddictConstants.Scopes.OpenId,
                    OpenIddictConstants.Scopes.Email,
                    OpenIddictConstants.Scopes.Profile,
                    OpenIddictConstants.Scopes.OfflineAccess,
                    "api");

                options
                    .SetAccessTokenLifetime(TimeSpan.FromMinutes(60))
                    .SetRefreshTokenLifetime(TimeSpan.FromDays(14))
                    .UseReferenceRefreshTokens();

                options.RegisterClaims(
                    OpenIddictConstants.Claims.Email,
                    OpenIddictConstants.Claims.Name,
                    "fullName");

                options.SetTokenEndpointUris("/api/Auth/Token");
                options.SetUserInfoEndpointUris("/api/Auth/UserInfo");

                options.AllowPasswordFlow();
                options.AllowRefreshTokenFlow();
                options.AcceptAnonymousClients();

                options.AddEphemeralEncryptionKey()
                    .AddEphemeralSigningKey();

                options.UseAspNetCore()
                    .EnableTokenEndpointPassthrough()
                    .EnableUserInfoEndpointPassthrough();
            })
            .AddValidation(options =>
            {
                options.UseLocalServer();
                options.UseAspNetCore();
            });

        return services;
    }
}