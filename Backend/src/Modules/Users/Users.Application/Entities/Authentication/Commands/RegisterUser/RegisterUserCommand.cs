using BuildingBlocks.Application.Cqrs;

namespace Users.Application.Entities.Authentication.Commands.RegisterUser;
public record RegisterUserCommand : ICommand
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
