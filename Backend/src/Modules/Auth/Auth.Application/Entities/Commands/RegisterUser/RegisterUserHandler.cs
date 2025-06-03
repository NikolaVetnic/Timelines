using Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Auth.Application.Entities.Commands.RegisterUser;

internal class RegisterUserHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            UserName = command.Username,
            Email = command.Email,
            FullName = command.FullName
        };

        var result = await userManager.CreateAsync(user, command.Password);

        return new RegisterUserResult
        {
            Succeeded = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description)
        };
    }
}
