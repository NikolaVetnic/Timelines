using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace Files.Application.Entities.Files.Commands.ReviveFileAsset;

public record ReviveFileAssetCommand(FileAssetId Id) : ICommand<ReviveFileAssetResponse>
{
    public ReviveFileAssetCommand(string Id) : this(FileAssetId.Of(Guid.Parse(Id))) { }
}

public record ReviveFileAssetResponse(bool FileRevived);

public class ReviveFileAssetCommandValidator : AbstractValidator<ReviveFileAssetCommand>
{
    public ReviveFileAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
