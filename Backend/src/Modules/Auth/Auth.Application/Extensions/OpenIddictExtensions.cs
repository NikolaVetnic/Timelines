using System.Security.Claims;
using OpenIddict.Abstractions;

namespace Auth.Application.Extensions;

public static class OpenIddictExtensions
{
    public static void MapDefaultDestinations(this ClaimsPrincipal principal)
    {
        principal.SetDestinations(claim => claim.Type switch
        {
            // Subject in both tokens
            OpenIddictConstants.Claims.Subject =>
            [
                OpenIddictConstants.Destinations.AccessToken,
                OpenIddictConstants.Destinations.IdentityToken
            ],

            // Username if profile was granted → both tokens
            OpenIddictConstants.Claims.Username when principal.HasScope(OpenIddictConstants.Scopes.Profile) =>
            [
                OpenIddictConstants.Destinations.AccessToken,
                OpenIddictConstants.Destinations.IdentityToken
            ],

            // Email if email scope → only access token
            OpenIddictConstants.Claims.Email when principal.HasScope(OpenIddictConstants.Scopes.Email) =>
            [
                OpenIddictConstants.Destinations.AccessToken
            ],

            // Custom fullName if profile → only access token
            "fullName" when principal.HasScope(OpenIddictConstants.Scopes.Profile) =>
            [
                OpenIddictConstants.Destinations.AccessToken
            ],

            // Everything else only in access token
            _ =>
            [
                OpenIddictConstants.Destinations.AccessToken
            ]
        });
    }
}