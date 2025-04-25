// ReSharper disable ClassNeverInstantiated.Global

using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;

namespace Files.Application.Entities.Files.Queries.GetFileAssetById;

public record GetFileAssetByIdQuery(FileAssetId Id) : IQuery<GetFileAssetByIdResult>
{
    public GetFileAssetByIdQuery(string Id) : this(FileAssetId.Of(Guid.Parse(Id))) { }
}

// ReSharper disable once NotAccessedPositionalProperty.Global

public record GetFileAssetByIdResult(FileAssetDto FileAssetDto);

public class GetFileAssetByIdQueryValidator : AbstractValidator<GetFileAssetByIdQuery>
{
    public GetFileAssetByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .Must(value => Guid.TryParse(value.ToString(), out _)).WithMessage("Id is not valid.");
    }
}
