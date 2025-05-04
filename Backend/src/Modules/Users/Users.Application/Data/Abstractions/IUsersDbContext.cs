using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;

namespace Users.Application.Data.Abstractions;

public interface IUsersDbContext
{
    DbSet<ApplicationUser> Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
