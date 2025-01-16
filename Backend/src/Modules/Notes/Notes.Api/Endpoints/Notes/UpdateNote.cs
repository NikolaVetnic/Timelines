using Notes.Application.Entities.Notes.Commands.UpdateNote;

namespace Notes.Api.Endpoints.Notes;

public class UpdateNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Notes", async (UpdateNoteRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateNoteCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateNoteResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateNote")
            .Produces<UpdateNoteResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Note")
            .WithDescription("Update Note");
    }
}

public record UpdateNoteRequest(NoteDto Note);

public record UpdateNoteResponse(bool NoteUpdated);
