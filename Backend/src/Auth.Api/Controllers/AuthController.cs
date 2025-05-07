using System.Threading.Tasks;
using Auth.Application.Data.Abstractions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;

// ReSharper disable InvertIf

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ITokenService tokenService)
    : ControllerBase
{
    [HttpPost("Token")]
    public async Task<IActionResult> Exchange()
    {
        var request = HttpContext.GetOpenIddictServerRequest()!;

        if (request.IsPasswordGrantType())
        {
            var principal = await tokenService.CreatePasswordGrantPrincipal(request.Username!, request.Password!);

            if (principal is null)
                return Forbid();

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        if (request.IsRefreshTokenGrantType())
        {
            var current = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme))
                .Principal!;
            var principal = await tokenService.RefreshTokenPrincipal(current);

            if (principal is null)
                return Forbid();

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        return BadRequest("Unsupported grant type.");
    }

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpPost("UserInfo"), Produces("application/json")]
    public Task<IActionResult> UserInfo()
    {
        var principal = HttpContext.User;

        return Task.FromResult<IActionResult>(Ok(new
        {
            sub = principal.GetClaim(OpenIddictConstants.Claims.Subject),
            username = principal.GetClaim(OpenIddictConstants.Claims.Username),
            fullName = principal.GetClaim("fullName"),
            email = principal.GetClaim(OpenIddictConstants.Claims.Email),
        }));
    }
}