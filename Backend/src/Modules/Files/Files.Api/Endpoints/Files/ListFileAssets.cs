using BuildingBlocks.Application.Pagination;
using Files.Application.Entities.Files.Queries.ListFileAssets;

namespace Files.Api.Endpoints.Files;

public class ListFileAssets : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Files", async ([AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListFileAssetsQuery(query));
                var response = result.Adapt<ListFileAssetsResponse>();

                return Results.Ok(response);
            })
            .WithName("ListFileAssets")
            .Produces<ListFileAssetsResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List File Assets")
            .WithDescription("List File Assets");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListFileAssetsResponse(PaginatedResult<FileAssetDto> FileAssets);
