using BuildingBlocks.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Threading.Tasks;
using BuildingBlocks.Domain.Timelines.Phase.ValueObjects;
using Timelines.Application.Entities.Phases.Commands.CreatePhase;
using Timelines.Application.Entities.Phases.Commands.DeletePhase;
using Timelines.Application.Entities.Phases.Commands.RevivePhase;
using Timelines.Application.Entities.Phases.Commands.UpdatePhase;
using Timelines.Application.Entities.Phases.Queries.GetPhaseById;
using Timelines.Application.Entities.Phases.Queries.ListFlaggedForDeletionPhases;
using Timelines.Application.Entities.Phases.Queries.ListNodesByPhaseId;
using Timelines.Application.Entities.Phases.Queries.ListPhases;

namespace Timelines.Api.Controllers.Phases;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class PhasesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreatePhaseResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatePhaseResponse>> Create([FromBody] CreatePhaseRequest request)
    {
        var command = request.Adapt<CreatePhaseCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreatePhaseResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{phaseId}")]
    [ProducesResponseType(typeof(GetPhaseByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetPhaseByIdResponse>> GetById(
        [FromRoute] string phaseId)
    {
        var result = await sender.Send(new GetPhaseByIdQuery(phaseId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetPhaseByIdResponse>();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListPhasesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListPhasesResponse>> List([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListPhasesQuery(query));
        var response = result.Adapt<ListPhasesResponse>();

        return Ok(response);
    }

    [HttpGet("Entity/Deleted")]
    [ProducesResponseType(typeof(ListPhasesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListPhasesResponse>> ListFlaggedForDeletion([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFlaggedForDeletionPhasesQuery(query));
        var response = result.Adapt<ListPhasesResponse>();

        return Ok(response);
    }

    [HttpGet("{phaseId}/Nodes")]
    [ProducesResponseType(typeof(ListNodesByPhaseIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNodesByPhaseIdResponse>> GetNodes(string phaseId, [FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListNodesByPhaseIdQuery(phaseId, query));
        var response = result.Adapt<ListNodesByPhaseIdResponse>();

        return Ok(response);
    }

    [HttpPut("{phaseId}")]
    [ProducesResponseType(typeof(UpdatePhaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdatePhaseResponse>> Update([FromRoute] string phaseId, [FromBody] UpdatePhaseRequest request)
    {
        var command = new UpdatePhaseCommand
        {
            Id = PhaseId.Of(Guid.Parse(phaseId)),
            Title = request.Title,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Duration = request.Duration,
            Status = request.Status,
            Progress = request.Progress,
            IsCompleted = request.IsCompleted,
            DependsOn = request.DependsOn,
            AssignedTo = request.AssignedTo,
            Stakeholders = request.Stakeholders,
            Tags = request.Tags,
            TimelineId = request.TimelineId
        };

        var result = await sender.Send(command);
        var response = result.Adapt<UpdatePhaseResponse>();

        return Ok(response);
    }

    [HttpDelete("{phaseId}")]
    [ProducesResponseType(typeof(DeletePhaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeletePhaseResponse>> Delete([FromRoute] string phaseId)
    {
        var result = await sender.Send(new DeletePhaseCommand(phaseId));
        var response = result.Adapt<DeletePhaseResponse>();

        return Ok(response);
    }

    [HttpPost("Entity/Deleted/Revive{phaseId}")]
    [ProducesResponseType(typeof(RevivePhaseResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RevivePhaseResponse>> Revive([FromRoute] string phaseId)
    {
        var result = await sender.Send(new RevivePhaseCommand(phaseId));
        var response = result.Adapt<RevivePhaseResponse>();

        return Ok(response);
    }
}
