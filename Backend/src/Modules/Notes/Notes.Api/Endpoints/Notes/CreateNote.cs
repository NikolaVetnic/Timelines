﻿using BuildingBlocks.Domain.ValueObjects.Ids;
using Notes.Application.Entities.Notes.Commands.CreateNote;

namespace Notes.Api.Endpoints.Notes;

public class CreateNote : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Notes", async (CreateNoteRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateNoteCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateNoteResponse>();

            return Results.Created($"/Notes/{response.Id}", response);
        })
            .WithName("CreateNote")
            .Produces<CreateNoteResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Note")
            .WithDescription("Creates a new note");
    }
}

public record CreateNoteRequest(NoteDto Note);

public record CreateNoteResponse(NoteId Id);
