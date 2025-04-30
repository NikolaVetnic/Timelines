namespace Users.Application.Data.Abstractions;

public interface IUsersDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
