using System;
using System.Threading.Tasks;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Notes.Note.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Entities.Notes.Commands.CreateNote;
using Notes.Application.Entities.Notes.Commands.DeleteNote;
using Notes.Application.Entities.Notes.Commands.ReviveNote;
using Notes.Application.Entities.Notes.Commands.UpdateNote;
using Notes.Application.Entities.Notes.Queries.GetNoteById;
using Notes.Application.Entities.Notes.Queries.ListFlaggedForDeletionNotes;
using Notes.Application.Entities.Notes.Queries.ListNotes;
using OpenIddict.Validation.AspNetCore;

namespace Notes.Api.Controllers.Notes;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class NotesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateNoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateNoteResponse>> Create([FromBody] CreateNoteRequest request)
    {
        var command = request.Adapt<CreateNoteCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateNoteResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{noteId}")]
    [ProducesResponseType(typeof(GetNoteByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetNoteByIdResponse>> GetById([FromRoute] string noteId)
    {
        var result = await sender.Send(new GetNoteByIdQuery(noteId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetNoteByIdResponse>();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListNotesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNotesResponse>> Get([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListNotesQuery(query));
        var response = result.Adapt<ListNotesResponse>();

        return Ok(response);
    }

    [HttpGet("Entity/Deleted")]
    [ProducesResponseType(typeof(ListNotesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNotesResponse>> GetFlaggedForDeletion([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFlaggedForDeletionNotesQuery(query));
        var response = result.Adapt<ListNotesResponse>();

        return Ok(response);
    }

    [HttpPut("{noteId}")]
    [ProducesResponseType(typeof(UpdateNoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateNoteResponse>> Update([FromRoute] string noteId, [FromBody] UpdateNoteRequest request)
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

        return Ok(response);
    }

    [HttpDelete("{noteId}")]
    [ProducesResponseType(typeof(DeleteNoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteNoteResponse>> Delete([FromRoute] string noteId)
    {
        var result = await sender.Send(new DeleteNoteCommand(noteId));
        var response = result.Adapt<DeleteNoteResponse>();

        return Ok(response);
    }

    [HttpPost("Entity/Deleted/Revive/{noteId}")]
    [ProducesResponseType(typeof(ReviveNoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviveNoteResponse>> Revive([FromRoute] string noteId)
    {
        var result = await sender.Send(new ReviveNoteCommand(noteId));
        var response = result.Adapt<ReviveNoteResponse>();

        return Ok(response);
    }
}
