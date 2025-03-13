using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace Files.Application.Entities.Files.Commands.CreateFileAsset;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateFileAssetCommand(FileAssetDto FileAsset) : ICommand<CreateFileAssetResult>;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateFileAssetResult(FileAssetId Id);

public class CreateFileCommandValidator : AbstractValidator<CreateFileAssetCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(x => x.FileAsset.Name).NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.FileAsset.Size)
            .NotNull().WithMessage("Size is required.")
            .GreaterThan(0).WithMessage("Size must be greater than 0.");

        RuleFor(x => x.FileAsset.Type)
            .NotEmpty().WithMessage("Type is required.");

        RuleFor(x => x.FileAsset.Owner)
            .NotEmpty().WithMessage("Owner is required.")
            .MaximumLength(100).WithMessage("Owner must not exceed 100 characters.");

        RuleFor(x => x.FileAsset.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
