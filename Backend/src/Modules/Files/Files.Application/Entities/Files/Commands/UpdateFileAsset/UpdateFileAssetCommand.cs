namespace Files.Application.Entities.Files.Commands.UpdateFileAsset;

public record UpdateFileAssetCommand(FileAssetDto FileAsset) : ICommand<UpdateFileAssetResult>;

public record UpdateFileAssetResult(bool FileAssetUpdated);

public class UpdateFileAssetCommandValidator : AbstractValidator<UpdateFileAssetCommand>
{
    public UpdateFileAssetCommandValidator()
    {
        RuleFor(x => x.FileAsset.Id)
            .NotNull().WithMessage("Id is required.");

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

