using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.Threading.Tasks;
using Nodes.Application.Entities.Nodes.Commands.CreateNode;
using Nodes.Application.Entities.Nodes.Queries.GetNodeById;

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
}