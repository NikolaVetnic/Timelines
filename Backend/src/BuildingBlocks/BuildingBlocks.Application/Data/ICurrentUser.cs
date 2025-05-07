using System.Security.Claims;

namespace BuildingBlocks.Application.Data;

public interface ICurrentUser
{
    ClaimsPrincipal? Principal { get; }
    string? UserId { get; }
}