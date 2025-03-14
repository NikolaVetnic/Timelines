using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.Dtos;
using Notes.Application.Entities.Notes.Queries.ListNotes;

namespace Notes.Api.Endpoints.Notes;

public class ListNotes : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Notes", async ([AsParameters] PaginationRequest query, ISender sender) =>
            {
                var result = await sender.Send(new ListNotesQuery(query));
                var response = result.Adapt<ListNotesResponse>();

                return Results.Ok(response);
            })
            .WithName("ListNotes")
            .Produces<ListNotesResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("List Notes")
            .WithDescription("List Notes");
    }
}

public record ListNotesResponse(PaginatedResult<NoteDto> Notes);
