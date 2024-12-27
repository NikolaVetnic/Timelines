using BuildingBlocks.Domain.ValueObjects.Ids;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Notes.Application.Dtos;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.Api.Endpoints;

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
