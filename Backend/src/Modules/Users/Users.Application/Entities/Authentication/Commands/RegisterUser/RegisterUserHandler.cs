using BuildingBlocks.Application.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Models;

namespace Users.Application.Entities.Authentication.Commands.RegisterUser;
internal class RegisterUserHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<Unit> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded) return new AuthResponse { Success = true };

        var errors = result.Errors.Select(e => e.Description);
        return new AuthResponse { Success = false, Errors = errors };

        // Optionally add default role
        //await _userManager.AddToRoleAsync(user, "User");

    }
}
