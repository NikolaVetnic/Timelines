using Authentication.Domain.Models;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;

namespace Authentication.Api.Endpoints.Authentication;

public class Register : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/register", async (RegisterRequest request, UserManager<ApplicationUser> userManager) =>
            {
                var user = new ApplicationUser
                {
                    UserName = request.Email,
                    Email = request.Email
                };
                var result = await userManager.CreateAsync(user, request.Password);

                return !result.Succeeded ? Results.BadRequest(result.Errors) : Results.Ok(new {Message = "Registration successful"});
            })
            .WithName("Register")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Register User")
            .WithDescription("Registers a new User");
    }
}

public record RegisterRequest(
    string Email,
    string Password,
    string ConfirmPassword);
