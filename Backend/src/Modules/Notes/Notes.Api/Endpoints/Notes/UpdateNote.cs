using System;
using System.Collections.Generic;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Notes.Application.Entities.Notes.Commands.UpdateNote;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Notes.Api.Endpoints.Notes;

public class UpdateNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/Notes/{noteId}", async (string noteId, UpdateNoteRequest request, ISender sender) =>
            {
                var command = new UpdateNoteCommand
                {
                    Id = NoteId.Of(Guid.Parse(noteId)),
                    Title = request.Title,
                    Content = request.Content,
                    Timestamp = request.Timestamp,
                    SharedWith = request.SharedWith,
                    IsPublic = request.IsPublic,
                    NodeId = request.NodeId
                };

                var result = await sender.Send(command);
                var response = result.Adapt<UpdateNoteResponse>();
                
                return Results.Ok(response);
            })
            .WithName("UpdateNote")
            .Produces<UpdateNoteResponse>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Note")
            .WithDescription("Updates a note");
    }
}

public record UpdateNoteRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public List<string> SharedWith { get; set; }
    public bool IsPublic { get; set; }
    public NodeId NodeId { get; set; }
}

// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once NotAccessedPositionalProperty.Global
public record UpdateNoteResponse(NoteDto NoteDto);
