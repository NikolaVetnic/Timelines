using System.Threading.Tasks;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Timelines.Timeline.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using Timelines.Application.Entities.Timelines.Queries.GetTimelineById;
using Timelines.Application.Entities.Timelines.Queries.ListTimelines;

namespace Timelines.Api.Controllers;

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
        var result   = await sender.Send(new ListTimelinesQuery(query));
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
}

public record ListTimelinesResponse(PaginatedResult<TimelineDto> Timelines);
public record GetTimelineByIdResponse(TimelineDto Timeline);