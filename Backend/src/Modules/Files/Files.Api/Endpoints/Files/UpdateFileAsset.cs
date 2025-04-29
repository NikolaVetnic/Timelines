using System;
using System.Collections.Generic;
using BuildingBlocks.Domain.Files.File.Dtos;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Files.Application.Entities.Files.Commands.UpdateFileAsset;

namespace Files.Api.Endpoints.Files;

public class UpdateFileAsset : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Files/{fileId}", async (string fileId, CreateFileAssetRequest request, ISender sender) =>
            {
                var command = new UpdateFileAssetCommand
                {
                    Id = FileAssetId.Of(Guid.Parse(fileId)),
                    Name = request.Name,
                    Description = request.Description,
                    SharedWith = request.SharedWith,
                    IsPublic = request.IsPublic,
                    NodeId = request.NodeId
                };
                
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateFileAssetResponse>();
                
                return Results.Ok(response);
            })
            .WithName("UpdateFileAsset")
            .Produces<UpdateFileAssetResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update File Asset")
            .WithDescription("Updates a file asset");
    }
}

// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateFileAssetRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateFileAssetResponse(FileAssetDto FileAsset);
