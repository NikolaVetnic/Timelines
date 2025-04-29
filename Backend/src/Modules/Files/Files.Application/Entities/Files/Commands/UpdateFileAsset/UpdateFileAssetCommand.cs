using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

namespace Files.Application.Entities.Files.Commands.UpdateFileAsset;

// ReSharper disable once ClassNeverInstantiated.Global
public record UpdateFileAssetCommand : ICommand<UpdateFileAssetResult>
{
    public required FileAssetId Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? SharedWith { get; set; }
    public bool? IsPublic { get; set; }
    public NodeId? NodeId { get; set; }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateFileAssetResult(FileAssetDto FileAsset);

public class UpdateFileCommandValidator : AbstractValidator<UpdateFileAssetCommand>
{
    public UpdateFileCommandValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
