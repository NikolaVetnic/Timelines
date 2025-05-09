using Microsoft.AspNetCore.Identity;

namespace Auth.Domain.Models;

public class ApplicationUser : IdentityUser
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public string? FullName { get; set; }
}
