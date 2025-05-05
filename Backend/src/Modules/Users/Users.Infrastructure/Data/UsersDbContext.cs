using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BuildingBlocks.Domain.Users.User.ValueObjects;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Users.Application.Data.Abstractions;
using Users.Domain.Models;

namespace Users.Infrastructure.Data;

public class UsersDbContext
    : IdentityDbContext<ApplicationUser>, IUsersDbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options){}

    public DbSet<ApplicationUser> Users { get; init; }

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
