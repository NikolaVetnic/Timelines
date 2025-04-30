using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Users.Application.Data.Abstractions;
using Users.Domain.Models;

namespace Users.Infrastructure.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IUsersDbContext;
