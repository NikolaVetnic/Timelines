using System.Security.Claims;
using BuildingBlocks.Application.Data;
using Microsoft.AspNetCore.Http;
using OpenIddict.Abstractions;

namespace Auth.Application.Data;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public ClaimsPrincipal? Principal { get; } = httpContextAccessor.HttpContext?.User;

    // Pick “sub” or NameIdentifier claim
    public string? UserId =>
        Principal?.FindFirst(OpenIddictConstants.Claims.Subject)?.Value ??
        Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
}