using BuildingBlocks.Domain.Enums;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Entities.Files.Commands.CreateFileAsset;
using System.Collections.Generic;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;

// ReSharper disable ClassNeverInstantiated.Global

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

                return Results.Created($"/Files/{response.Id}", response);
            })
            .WithName("CreateFileAsset")
            .Produces<CreateFileAssetResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create File Asset")
            .WithDescription("Creates a new file asset");
    }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record CreateFileAssetRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public float Size { get; set; }
    public EFileType Type { get; set; }
    public string Owner { get; set; }
    public byte[] Content { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}

public record CreateFileAssetResponse(FileAssetId Id);
