using BuildingBlocks.Domain.ValueObjects.Ids;
using Files.Application.Files.Commands.CreateFileAsset;

// ReSharper disable ClassNeverInstantiated.Global
using Files.Application.Entities.Files.Commands.CreateFileAsset;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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
public record CreateFileAssetResponse(FileAssetId Id);
