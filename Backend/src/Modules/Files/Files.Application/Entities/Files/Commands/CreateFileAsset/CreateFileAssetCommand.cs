using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Application.Entities.Files.Commands.CreateFileAsset;

// ReSharper disable once ClassNeverInstantiated.Global
public record CreateFileAssetCommand : ICommand<CreateFileAssetResult>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required float Size { get; set; }
    public required EFileType Type { get; set; }
    public required string Owner { get; set; }
    public required byte[] Content { get; set; }
    public List<string> SharedWith { get; set; }
    public required bool IsPublic { get; set; }
    public required NodeId NodeId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateFileAssetResult(FileAssetId Id);

public class CreateFileCommandValidator : AbstractValidator<CreateFileAssetCommand>
{
    public CreateFileCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.Size)
            .NotNull().WithMessage("Size is required.")
            .GreaterThan(0).WithMessage("Size must be greater than 0.");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required.");

        RuleFor(x => x.Owner)
            .NotEmpty().WithMessage("Owner is required.")
            .MaximumLength(100).WithMessage("Owner must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
