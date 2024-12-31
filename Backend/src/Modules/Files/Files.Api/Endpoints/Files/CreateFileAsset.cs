using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Entities.Files.Commands.CreateFileAsset;

namespace Files.Api.Endpoints.Files;

public class CreateFileAsset : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Files", async (CreateFileAssetRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateFileAssetCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateFileAssetResponse>();

                return Results.Created($"/Files/{response.AssetId}", response);
            })
            .WithName("CreateFileAsset")
            .Produces<CreateFileAssetResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create File Asset")
            .WithDescription("Creates a new file asset");
    }
}

public record CreateFileAssetRequest(FileAssetDto FileAsset);

public record CreateFileAssetResponse(FileAssetId AssetId);
