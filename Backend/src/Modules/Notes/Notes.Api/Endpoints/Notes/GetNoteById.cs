using System.Text.Json.Serialization;
using Notes.Application.Entities.Notes.Queries.GetNoteById;

namespace Notes.Api.Endpoints.Notes;

// ReSharper disable once UnusedType.Global
public class GetNoteById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/Notes/{noteId}", async (string noteId, ISender sender) =>
            {
                var result = await sender.Send(new GetNoteByIdQuery(noteId));
                var response = result.Adapt<GetNoteByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetNoteById")
            .Produces<GetNoteByIdResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Note by Id")
            .WithDescription("Get Note by Id");
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record GetNoteByIdResponse([property: JsonPropertyName("note")] NoteDto NoteDto);
