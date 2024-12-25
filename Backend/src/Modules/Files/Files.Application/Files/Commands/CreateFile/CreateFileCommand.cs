using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Dtos;
using FluentValidation;

namespace Files.Application.Files.Commands.CreateFile;

public record CreateFileCommand(FileAssetDto FileAsset) : ICommand<CreateFileResult>;

public record CreateFileResult(FileAssetId AssetId);

public class CreateFileCommandValidator : AbstractValidator<CreateFileCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(x => x.FileAsset.Title).NotEmpty().WithMessage("Title is required.");

        // ToDo: Add remaining File command validators
    }
}
