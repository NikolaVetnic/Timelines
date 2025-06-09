using System.Threading.Tasks;
using Auth.Application.Entities.Commands.RegisterUser;
using BuildingBlocks.Api.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(ISender sender) : ControllerBase
{
    [HttpPost("Register")]
    [RequireApiKey]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await sender.Send(command);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully.");
    }
}
