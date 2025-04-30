using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Application.Data.Abstractions;
using Users.Domain.Models;

namespace Users.Infrastructure.Data;

public class UsersDbContext(DbContextOptions<UsersDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IUsersDbContext
{
    public DbSet<ApplicationUser> ApplicationUsers { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Specify schema for this module
        builder.HasDefaultSchema("Users");

        // Configure entities
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.ToTable("Users"); // Specify table name within the schema
        });

        // Apply all configurations taken from classes that implement IEntityTypeConfiguration<>
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
