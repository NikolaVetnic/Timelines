using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Threading.Tasks;
using Nodes.Application.Entities.Nodes.Commands.CreateNode;
using Nodes.Application.Entities.Nodes.Queries.GetNodeById;
using BuildingBlocks.Application.Pagination;
using Nodes.Application.Entities.Nodes.Queries.ListFileAssetsByNodeId;
using Nodes.Application.Entities.Nodes.Queries.ListNodes;
using Nodes.Application.Entities.Nodes.Queries.ListNotesByNodeId;
using Nodes.Application.Entities.Nodes.Queries.ListRemindersByNodeId;
using System;
using BuildingBlocks.Domain.Nodes.Node.ValueObjects;
using Nodes.Application.Entities.Nodes.Commands.DeleteNode;
using Nodes.Application.Entities.Nodes.Commands.ReviveNode;
using Nodes.Application.Entities.Nodes.Commands.UpdateNode;
using Nodes.Application.Entities.Nodes.Queries.ListFlaggedForDeletionNodes;

namespace Nodes.Api.Controllers.Nodes;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class NodesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateNodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateNodeResponse>> Create([FromBody] CreateNodeRequest request)
    {
        var command = request.Adapt<CreateNodeCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateNodeResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{nodeId}")]
    [ProducesResponseType(typeof(GetNodeByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetNodeByIdResponse>> GetById([FromRoute] string nodeId)
    {
        var result = await sender.Send(new GetNodeByIdQuery(nodeId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetNodeByIdResponse>();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListNodesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNodesResponse>> Get([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListNodesQuery(query));
        var response = result.Adapt<ListNodesResponse>();

        return Ok(response);
    }

    [HttpGet("Entity/Deleted")]
    [ProducesResponseType(typeof(ListNodesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNodesResponse>> GetFlaggedForDeletion([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFlaggedForDeletionNodesQuery(query));
        var response = result.Adapt<ListNodesResponse>();

        return Ok(response);
    }

    [HttpGet("{nodeId}/Files")]
    [ProducesResponseType(typeof(ListFileAssetsByNodeIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListFileAssetsByNodeIdResponse>> GetFiles(string nodeId, [FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFileAssetsByNodeIdQuery(nodeId, query));
        var response = result.Adapt<ListFileAssetsByNodeIdResponse>();

        return Ok(response);
    }

    [HttpGet("{nodeId}/Notes")]
    [ProducesResponseType(typeof(ListNotesByNodeIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListNotesByNodeIdResponse>> GetNotes(string nodeId, [FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListNotesByNodeIdQuery(nodeId, query));
        var response = result.Adapt<ListNotesByNodeIdResponse>();

        return Ok(response);
    }

    [HttpGet("{nodeId}/Reminders")]
    [ProducesResponseType(typeof(ListRemindersByNodeIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListRemindersByNodeIdResponse>> GetReminders(string nodeId, [FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListRemindersByNodeIdQuery(nodeId, query));
        var response = result.Adapt<ListRemindersByNodeIdResponse>();

        return Ok(response);
    }

    [HttpPut("{nodeId}")]
    [ProducesResponseType(typeof(UpdateNodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateNodeResponse>> Update([FromRoute] string nodeId, [FromBody] UpdateNodeRequest request)
    {
        var command = new UpdateNodeCommand
        {
            Id = NodeId.Of(Guid.Parse(nodeId)),
            Title = request.Title,
            Description = request.Description,
            Timestamp = request.Timestamp,
            Importance = request.Importance,
            Categories = request.Categories,
            Tags = request.Tags,
            TimelineId = request.TimelineId
        };

        var result = await sender.Send(command);
        var response = result.Adapt<UpdateNodeResponse>();

        return Ok(response);
    }

    [HttpDelete("{nodeId}")]
    [ProducesResponseType(typeof(DeleteNodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteNodeResponse>> Delete([FromRoute] string nodeId)
    {
        var result = await sender.Send(new DeleteNodeCommand(nodeId));
        var response = result.Adapt<DeleteNodeResponse>();

        return Ok(response);
    }

    [HttpPost("Entity/Deleted/Revive/{nodeId}")]
    [ProducesResponseType(typeof(ReviveNodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReviveNodeResponse>> Revive([FromRoute] string nodeId)
    {
        var result = await sender.Send(new ReviveNodeCommand(nodeId));
        var response = result.Adapt<ReviveNodeResponse>();

        return Ok(response);
    }
}
