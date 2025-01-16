using FluentValidation.Validators;

namespace Files.Application.Entities.Files.Commands.UpdateFileAsset;

public record UpdateFileAssetCommand : ICommand<UpdateFileAssetResult>
{
    public required string Id { get; set; }
    public string? Name { get; set; }
    public string? Size { get; set; }
    public string? Type { get; set; }
    public string? Owner { get; set; }
    public string? Description { get; set; }
    public List<string>? SharedWith { get; init; }
}

public record UpdateFileAssetResult(bool FileAssetUpdated);

public class UpdateFileAssetCommandValidator : AbstractValidator<UpdateFileAssetCommand>
{
    public UpdateFileAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("Id is required.");

        RuleFor(x => x.Type)
            .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

        RuleFor(x => x.Owner)
            .MaximumLength(100).WithMessage("Owner must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
