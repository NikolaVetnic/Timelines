using Notes.Application.Entities.Notes.Commands.DeleteNote;

namespace Notes.Api.Endpoints.Notes;

public class DeleteNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/Notes/{noteId}", async (string noteId, ISender sender) =>
            {
                var result = await sender.Send(new DeleteNoteCommand(noteId));
                var response = result.Adapt<DeleteNoteResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteNote")
            .Produces<DeleteNoteResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Note")
            .WithDescription("Delete Note");
    }
}

public record DeleteNoteResponse(bool NoteDeleted);
