using System.Security.Claims;

namespace Auth.Application.Data.Abstractions;

public interface ITokenService
{
    Task<ClaimsPrincipal?> CreatePasswordGrantPrincipal(string userName, string password);
    Task<ClaimsPrincipal?> RefreshTokenPrincipal(ClaimsPrincipal current);
}
