using Auth.Domain.Models;
using Auth.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateAuthDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var context = scopedProvider.GetRequiredService<AuthDbContext>();

        await context.Database.MigrateAsync();
    }

    public static async Task MigrateAndSeedAuthDatabaseAsync(this IServiceProvider services)
    {
        await services.MigrateAuthDatabaseAsync();
        await services.SeedAsync();
    }

    private static async Task SeedAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var seededUser in InitialData.SeededUsers)
        {
            if (await userManager.FindByNameAsync(seededUser.Username) is not null)
                continue;

            var user = new ApplicationUser
            {
                Id = seededUser.Id,
                UserName = seededUser.Username,
                Email = seededUser.Email,
                FullName = seededUser.FullName
            };

            var result = await userManager.CreateAsync(user, seededUser.Password);

            if (result.Succeeded)
                continue;

            var errors = string.Join("; ", result.Errors.Select(e => e.Description));

            throw new InvalidOperationException($"Seeding {seededUser.Username} failed: {errors}");
        }
    }
}
