using System.Threading.Tasks;
using Auth.Application.Entities.Commands.RegisterUser;
using Auth.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(ISender sender, UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterUserCommand command)
    {
        var result = await sender.Send(command);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully.");
    }
}