using System.Collections.Generic;
using Files.Application.Entities.Files.Commands.UpdateFileAsset;

namespace Files.Api.Endpoints.Files;

public class UpdateFileAsset : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Files", async (UpdateFileAssetRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateFileAssetCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateFileAssetResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateFileAsset")
            .Produces<UpdateFileAssetResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update FileAsset")
            .WithDescription("Update FileAsset");
    }
}

public record UpdateFileAssetRequest
{
    public required string Id { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public string Type { get; set; }
    public string Owner { get; set; }
    public string Description { get; set; }
    public List<string> SharedWith { get; init; }
}

public record UpdateFileAssetResponse(bool FileAssetUpdated);
