using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using Nodes.Application.Entities.Nodes.Queries.ListNotesByNodeId;

namespace Nodes.Api.Endpoints.Nodes;

public class ListNotesByNodeId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Nodes/{nodeId}/Notes", async (string nodeId, [AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListNotesByNodeIdQuery(nodeId, query));
                var response = result.Adapt<ListNotesByNodeIdResponse>();
                return Results.Ok(response);
            })
            .WithName("ListNotesByNodeId")
            .Produces<ListFileAssetsByNodeIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Note by Node Id")
            .WithDescription("List Note by Node Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListNotesByNodeIdResponse(PaginatedResult<NoteBaseDto> Notes);
