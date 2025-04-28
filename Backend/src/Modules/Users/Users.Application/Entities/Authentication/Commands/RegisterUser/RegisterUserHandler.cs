using BuildingBlocks.Application.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Users.Domain.Models;

namespace Users.Application.Entities.Authentication.Commands.RegisterUser;
internal class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return new AuthResponse { Success = false, Errors = errors };
        }

        // Optionally add default role
        await _userManager.AddToRoleAsync(user, "User");

        return new AuthResponse { Success = true };
    }
}
