namespace BuildingBlocks.Domain.Exceptions
{
    public class DomainException(string message)
        : Exception($"Domain Exception: \"{message}\" throws from Domain Layer.");
}
