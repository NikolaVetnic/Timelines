using System.Security.Principal;
using BuildingBlocks.Application.Cqrs;

namespace Users.Application.Entities.Authentication.Commands.RegisterUser;

public record RegisterUserCommand : ICommand<RegisterUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record RegisterUserResponse
{
    public bool Success { get; set; }
    public IEnumerable<string> Errors { get; set; }
}
