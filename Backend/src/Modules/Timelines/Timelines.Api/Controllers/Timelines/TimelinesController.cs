using System;
using System.Threading.Tasks;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Timelines.Application.Entities.Timelines.Commands.CreateTimeline;
using Timelines.Application.Entities.Timelines.Commands.CreateTimelineWithTemplate;
using Timelines.Application.Entities.Timelines.Commands.DeleteTimeline;
using Timelines.Application.Entities.Timelines.Commands.UpdateTimeline;
using Timelines.Application.Entities.Timelines.Queries.GetTimelineById;
using Timelines.Application.Entities.Timelines.Queries.ListFlaggedForDeletionTimelines;
using Timelines.Application.Entities.Timelines.Queries.ListNodesByTimelineId;
using Timelines.Application.Entities.Timelines.Queries.ListTimelines;

namespace Timelines.Api.Controllers.Timelines;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class TimelinesController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ListTimelinesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListTimelinesResponse>> Get([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListTimelinesQuery(query));
        var response = result.Adapt<ListTimelinesResponse>();

        return Ok(response);
    }

    [HttpGet("Entity/Deleted")]
    [ProducesResponseType(typeof(ListTimelinesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListFlaggedForDeletionTimelinesResponse>> GetFlaggedForDeletion([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFlaggedForDeletionTimelinesQuery(query));
        var response = result.Adapt<ListTimelinesResponse>();

        return Ok(response);
    }

    [HttpGet("{timelineId}")]
    [ProducesResponseType(typeof(GetTimelineByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetTimelineByIdResponse>> GetById(
        [FromRoute] string timelineId)
    {
        var result = await sender.Send(new GetTimelineByIdQuery(timelineId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetTimelineByIdResponse>();

        return Ok(response);
    }

    [HttpGet("{timelineId}/Nodes")]
    [ProducesResponseType(typeof(ListNodesByTimelineIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNodesByTimelineIdResponse>> ListNodesByTimelineId(
        [FromRoute] string timelineId,
        [FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListNodesByTimelineIdQuery(timelineId, query));
        var response = result.Adapt<ListNodesByTimelineIdResponse>();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateTimelineResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateTimelineResponse>> Create(
        [FromBody] CreateTimelineRequest request)
    {
        var command = request.Adapt<CreateTimelineCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateTimelineResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { timelineId = response.Id },
            response);
    }

    [HttpPost("Clone/{timelineId}")]
    [ProducesResponseType(typeof(CreateTimelineWithTemplateResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateTimelineWithTemplateResponse>> Clone(
        [FromRoute] string timelineId,
        [FromBody] CreateTimelineWithTemplateRequest request)
    {
        var command = new CreateTimelineWithTemplateCommand
        {
            Id = TimelineId.Of(Guid.Parse(timelineId)),
            Title = request.Title,
            Description = request.Description
        };
        var result = await sender.Send(command);
        var response = result.Adapt<CreateTimelineWithTemplateResponse>();

        return CreatedAtAction(
            nameof(GetById),
            new { timelineId = response.Timeline.Id },
            response);
    }

    [HttpPut("{timelineId}")]
    [ProducesResponseType(typeof(UpdateTimelineResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateTimelineResponse>> Update(
        [FromRoute] string timelineId,
        [FromBody] UpdateTimelineRequest request)
    {
        var command = new UpdateTimelineCommand
        {
            Id = TimelineId.Of(Guid.Parse(timelineId)),
            Title = request.Title,
            Description = request.Description
        };
        var result = await sender.Send(command);
        var response = result.Adapt<UpdateTimelineResponse>();

        return Ok(response);
    }

    [HttpDelete("{timelineId}")]
    [ProducesResponseType(typeof(DeleteTimelineResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteTimelineResponse>> Delete(
        [FromRoute] string timelineId)
    {
        var result = await sender.Send(new DeleteTimelineCommand(timelineId));
        var response = result.Adapt<DeleteTimelineResponse>();

        return Ok(response);
    }
}
