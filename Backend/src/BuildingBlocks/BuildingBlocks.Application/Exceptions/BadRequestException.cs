namespace BuildingBlocks.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }

    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Details { get; }
}
