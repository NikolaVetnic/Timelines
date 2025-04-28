using BuildingBlocks.Application.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Models;

namespace Users.Application.Entities.Authentication.Commands.LoginUser;
internal class LoginUserHandler(UserManager<ApplicationUser> userManager) : ICommandHandler<LoginUserCommand, AuthenticationResponse>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public async Task<AuthenticationResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);

        if (user == null)
            return new AuthenticationResponse { Success = false, Error = "User not found" };

        var passwordValid = await _userManager.CheckPasswordAsync(user, command.Password);

        if (!passwordValid)
            return new AuthenticationResponse { Success = false, Error = "Invalid password" };

        var token = await GenerateJwtToken(user);

        return new AuthenticationResponse
        {
            Success = true,
            //Token = token,
            //UserId = user.Id,
            //Email = user.Email
        };
    }
}
