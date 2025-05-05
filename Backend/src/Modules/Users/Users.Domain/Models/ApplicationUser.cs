using BuildingBlocks.Domain.Users.User.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Users.Domain.Models;

public class ApplicationUser : IdentityUser;

public class ApplicationRole : IdentityRole<UserId>;
