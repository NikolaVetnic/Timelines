using BuildingBlocks.Application.Cqrs;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Models;

namespace Users.Application.Entities.Authentication.Commands.RegisterUser;
internal class RegisterUserHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<RegisterUserResponse> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = command.Email,
            UserName = command.Email
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (result.Succeeded) return new RegisterUserResponse { Success = true };

        var errors = result.Errors.Select(e => e.Description);
        return new RegisterUserResponse { Success = false, Errors = errors };

        // Optionally add default role
        //await _userManager.AddToRoleAsync(user, "User");
    }
}
