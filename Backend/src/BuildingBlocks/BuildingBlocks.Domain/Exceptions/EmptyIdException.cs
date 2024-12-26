namespace BuildingBlocks.Domain.Exceptions
{
    public class EmptyIdException(string message)
        : Exception($"Domain Exception: \"{message}\" throws from Domain Layer.");
}
