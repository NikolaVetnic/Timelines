using System;
using System.Threading.Tasks;
using BuildingBlocks.Application.Pagination;
using BuildingBlocks.Domain.Files.File.ValueObjects;
using Files.Application.Entities.Files.Commands.CreateFileAsset;
using Files.Application.Entities.Files.Commands.DeleteFileAsset;
using Files.Application.Entities.Files.Commands.UpdateFileAsset;
using Files.Application.Entities.Files.Queries.GetFileAssetById;
using Files.Application.Entities.Files.Queries.ListFileAssets;
using Files.Application.Entities.Files.Queries.ListFlaggedForDeletionFileAssets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;

namespace Files.Api.Controllers.Files;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
public class FilesController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(CreateFileAssetResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreateFileAssetResponse>> Create(
        [FromBody] CreateFileAssetRequest request)
    {
        var command = request.Adapt<CreateFileAssetCommand>();
        var result = await sender.Send(command);
        var response = result.Adapt<CreateFileAssetResponse>();

        return CreatedAtAction(nameof(Create), new { id = response.Id }, response);
    }

    [HttpGet("{fileId}")]
    [ProducesResponseType(typeof(GetFileAssetByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetFileAssetByIdResponse>> GetById([FromRoute] string fileId)
    {
        var result = await sender.Send(new GetFileAssetByIdQuery(fileId));

        if (result is null)
            return NotFound();

        var response = result.Adapt<GetFileAssetByIdResponse>();

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListFileAssetsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListFileAssetsResponse>> Get([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFileAssetsQuery(query));
        var response = result.Adapt<ListFileAssetsResponse>();

        return Ok(response);
    }

    [HttpGet("Entity/Deleted")]
    [ProducesResponseType(typeof(ListFileAssetsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ListFileAssetsResponse>> GetFlaggedForDeletion([FromQuery] PaginationRequest query)
    {
        var result = await sender.Send(new ListFlaggedForDeletionFileAssetsQuery(query));
        var response = result.Adapt<ListFileAssetsResponse>();

        return Ok(response);
    }

    [HttpPut("{fileId}")]
    [ProducesResponseType(typeof(UpdateFileAssetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UpdateFileAssetResponse>> Update([FromRoute] string fileId, [FromBody] UpdateFileAssetRequest request)
    {
        var command = new UpdateFileAssetCommand
        {
            Id = FileAssetId.Of(Guid.Parse(fileId)),
            Name = request.Name,
            Description = request.Description,
            SharedWith = request.SharedWith,
            IsPublic = request.IsPublic,
            NodeId = request.NodeId
        };

        var result = await sender.Send(command);
        var response = result.Adapt<UpdateFileAssetResponse>();

        return Ok(response);
    }

    [HttpDelete("{fileId}")]
    [ProducesResponseType(typeof(DeleteFileAssetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeleteFileAssetResponse>> Delete([FromRoute] string fileId)
    {
        var result = await sender.Send(new DeleteFileAssetCommand(fileId));
        var response = result.Adapt<DeleteFileAssetResponse>();

        return Ok(response);
    }
}
