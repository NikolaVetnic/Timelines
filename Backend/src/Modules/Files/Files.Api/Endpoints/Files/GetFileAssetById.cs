using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Files.File.Dtos;
using Files.Application.Entities.Files.Queries.GetFileAssetById;

namespace Files.Api.Endpoints.Files;

// ReSharper disable once UnusedType.Global
public class GetFileAssetById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Files/{fileId}", async (string fileId, ISender sender) =>
            {
                var result = await sender.Send(new GetFileAssetByIdQuery(fileId));
                var response = result.Adapt<GetFileAssetByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetFileAssetById")
            .Produces<GetFileAssetByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get File Asset by Id")
            .WithDescription("Get File Asset by Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetFileAssetByIdResponse([property: JsonPropertyName("fileAssets")] FileAssetDto FileAssetDto);
