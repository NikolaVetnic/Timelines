namespace Auth.Infrastructure.Extensions;

internal static class InitialData
{
    public static IEnumerable<SeededUser> SeededUsers =>
        new List<SeededUser>
        {
            new()
            {
                Id = "11111111-1111-1111-1111-111111111111",
                Username = "USRa",
                Email = "USRa@email.com",
                FullName = "User A",
                Password = "P@ssw0rd!123"
            },
            new()
            {
                Id = "22222222-2222-2222-2222-222222222222",
                Username = "USRb",
                Email = "USRb@email.com",
                FullName = "User B",
                Password = "P@ssw0rd?456"
            }
        };
}

public class SeededUser
{
    public required string Id { get; init; }
    public required string Username { get; init; }
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required string Password { get; init; }
}