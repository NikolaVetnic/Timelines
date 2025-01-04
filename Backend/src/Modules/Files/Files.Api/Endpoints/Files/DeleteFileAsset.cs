using Files.Application.Entities.Files.Commands.DeleteFileAsset;

namespace Files.Api.Endpoints.Files;

public class DeleteFileAsset : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Files/{fileId}", async (string fileId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteFileAssetCommand(fileId));
                var response = result.Adapt<DeleteFileAssetResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteFileAsset")
            .Produces<DeleteFileAssetResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete File Asset")
            .WithDescription("Delete File Asset");
    }
}

public record DeleteFileAssetResponse(bool IsSuccess);
