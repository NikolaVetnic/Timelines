namespace Files.Application.Entities.Files.Commands.DeleteFileAsset;

public record DeleteFileAssetCommand(string FileAssetId) : ICommand<DeleteFileAssetResult>;

public record DeleteFileAssetResult(bool IsSuccess);

public class DeleteFileAssetCommandValidator : AbstractValidator<DeleteFileAssetCommand>
{
    public DeleteFileAssetCommandValidator()
    {
        RuleFor(x => x.FileAssetId).NotEmpty().WithMessage("FileAssetId is required");
    }
}
