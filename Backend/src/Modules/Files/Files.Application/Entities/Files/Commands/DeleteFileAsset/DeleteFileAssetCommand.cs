namespace Files.Application.Entities.Files.Commands.DeleteFileAsset;

public record DeleteFileAssetCommand(string Id) : ICommand<DeleteFileAssetResult>;

public record DeleteFileAssetResult(bool FileDeleted);

public class DeleteFileAssetCommandValidator : AbstractValidator<DeleteFileAssetCommand>
{
    public DeleteFileAssetCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
