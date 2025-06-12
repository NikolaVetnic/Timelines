using BuildingBlocks.Application.Cqrs;
using FluentValidation;

namespace Auth.Application.Entities.Commands.RegisterUser;

public class RegisterUserCommand : ICommand<RegisterUserResult>
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

public class RegisterUserResult
{
    public bool Succeeded { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("Full name is required.");

        // ToDo: Add remaining RegisterUser command validators
    }
}
