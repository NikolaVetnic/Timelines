using BuildingBlocks.Application.Cqrs;

namespace Users.Application.Entities.Authentication.Commands.LoginUser;
public record LoginUserCommand : ICommand<AuthenticationResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record AuthenticationResponse
{
    public bool Success { get; set; }
    public string Error { get; set; }
}
