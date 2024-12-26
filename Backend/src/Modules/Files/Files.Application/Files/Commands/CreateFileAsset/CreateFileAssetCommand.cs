using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Dtos;
using FluentValidation;

namespace Files.Application.Files.Commands.CreateFileAsset;

public record CreateFileAssetCommand(FileAssetDto FileAsset) : ICommand<CreateFileResult>;

public record CreateFileResult(FileAssetId AssetId);

public class CreateFileCommandValidator : AbstractValidator<CreateFileAssetCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(x => x.FileAsset.Name).NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.FileAsset.Size)
            .NotNull().WithMessage("Size is required.")
            .GreaterThan(0).WithMessage("Size must be greater than 0.");

        RuleFor(x => x.FileAsset.Type)
            .NotEmpty().WithMessage("Type is required.")
            .MaximumLength(100).WithMessage("Type must not exceed 100 characters.");

        RuleFor(x => x.FileAsset.Owner)
            .NotEmpty().WithMessage("Owner is required.")
            .MaximumLength(100).WithMessage("Owner must not exceed 100 characters.");

        RuleFor(x => x.FileAsset.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
