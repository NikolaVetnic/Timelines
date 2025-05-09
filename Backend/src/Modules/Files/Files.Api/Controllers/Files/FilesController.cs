using System.Threading.Tasks;
using Files.Application.Entities.Files.Commands.CreateFileAsset;
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
}
