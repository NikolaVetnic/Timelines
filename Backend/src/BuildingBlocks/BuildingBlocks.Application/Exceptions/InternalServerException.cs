namespace BuildingBlocks.Application.Exceptions;

public class InternalServerException : Exception
{
    public InternalServerException(string message) : base(message) { }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Details { get; }
}
